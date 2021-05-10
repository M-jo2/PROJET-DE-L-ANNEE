using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyPopuStore.UI.Resource
{
    class Export
    {
        private class ProductInfo
        {
            public string Code { get; set; }
            public string Label { get; set; }
            public int SaleQuantity { get; set; }
            public int Stock { get; set; }
        }
        public string MyPopuStore_Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        private List<ProductInfo> ProductInfos;

        public Export()
        {
            ProductInfos = new();
        }

        public void ExportToHtml(string outputPath,string name,bool overwrite)
        {
            string exportPath = @$"{outputPath}\{name}";
            File.Copy(@".\UI\Resource\HTML_Template\MyPopupStore_ReportTemplate.html", exportPath, overwrite);

            string textHtml = File.ReadAllText(exportPath);

            textHtml = textHtml.Replace("MyPopupStore_Title", InfoServices.getPopupStoreInfo().PopupStoreName);
            textHtml = textHtml.Replace("MyPopupStore_Interval", $"{Start.ToString("dd MMMM yyyy")} - {End.ToString("dd MMMM yyyy")}");
            textHtml = textHtml.Replace("MyPopupStore_LineProduct", WriteLinesOfTable(CollectProduct()));
            textHtml = textHtml.Replace("MyPopupStore_Total", "1200");

            File.WriteAllText(exportPath, textHtml);


        }

        private List<ProductInfo> CollectProduct()
        {
            List<ProductInfo> productInfos = new();
            List<Product> products = ProductServices.GetAllProduct();

            foreach(Product product in products)
            {
                productInfos.Add(new ProductInfo { 
                    Code=product.Code,
                    Label = product.Label,
                    Stock = product.QuantityStock ?? default(int),
                    SaleQuantity = SaleServices.QuantitySoldOfAProduct(product.Code,Start,End),
                });
            }
            return productInfos;
        }

        private string WriteLinesOfTable(List<ProductInfo> productInfos)
        {
            string output = "";

            foreach (ProductInfo product in productInfos)
            {
                output += @$"<tr>
					        <td>{product.Code}</td>
					        <td>{product.Label}</td>
					        <td>{product.SaleQuantity}</td>
					        <td>{product.Stock}</td>
				            </tr>"+"\n";
            }

            return output;
        }
    }
}
