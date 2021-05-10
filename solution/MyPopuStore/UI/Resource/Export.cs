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

        public void ExportToHtml()
        {
            List<ProductInfo> productInfos = CollectProduct();

            foreach (ProductInfo product in productInfos)
            {
                Console.WriteLine($"{product.Code}  {product.Label}  {product.SaleQuantity}  {product.Stock}");
            }
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
    }
}
