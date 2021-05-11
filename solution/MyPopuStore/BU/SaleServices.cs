using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    using DAL.DB;
    using System.Runtime.InteropServices;

    class SaleServices
    {
        public static List<Sale> getAllSales()
        {
            
            using (MyPopupStoreDBContext db = new())
            {
                return db.Sales.ToList();
            }
        }
        public static int getNumberProductOneSale(int SaleID)
        {
            using (MyPopupStoreDBContext db = new())
            {
                int? quantity = 0;
                quantity = db.SaleDetails.Where(e => e.SaleId == SaleID).Sum(e=>e.NbProduct);
                if (quantity == null) throw new Exception("Quantité de produit null");
                return (int)quantity;
            }
        }
        public static decimal getTotalOneSale(int SaleID)
        {
            using (MyPopupStoreDBContext db = new())
            {
                decimal? total = 0;
                total = db.SaleDetails.Where(e => e.SaleId == SaleID).Sum(e => e.Price*e.NbProduct);
                return (decimal)total;
            }
        }
        public static List<SaleDetail> getSaleDetailsOneSale(int SaleID)
        {
            using (MyPopupStoreDBContext db = new())
            {
                List<SaleDetail> saleDetails = db.SaleDetails.Where(e => e.SaleId == SaleID).ToList();
                return saleDetails;
            }
        }
        public static bool EnoughProductInStock(List<SaleDetail> saleDetails)
        {
            Dictionary<string,int> codesQuantities = new Dictionary<string, int>();
            foreach (SaleDetail saleDetail in saleDetails)
            {
                if (codesQuantities.ContainsKey(saleDetail.ProductCode)) codesQuantities[saleDetail.ProductCode] += (int)saleDetail.NbProduct;
                else codesQuantities.Add(saleDetail.ProductCode, (int)saleDetail.NbProduct);
            }
            foreach (KeyValuePair<string,int> codeQuantity in codesQuantities)
            {
                Product product = ProductServices.GetProduct(codeQuantity.Key);
                if (product.QuantityStock - codeQuantity.Value < 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static void NewSale(List<SaleDetail> saleDetails,bool paymentType,bool decrementQuantityStock=true)
        {
            if(decrementQuantityStock && !EnoughProductInStock(saleDetails))
                throw new Exception("Produit manquant dans le stock.");

            using (MyPopupStoreDBContext db = new())
            {
                
                Sale sale = new()
                {
                    PaymentType = paymentType,
                    Date = DateTime.Now
                };
                sale.SaleDetails = saleDetails;
                
                db.Sales.Add(sale);
                db.SaveChanges();
            }

            if (decrementQuantityStock)
            {
                foreach (SaleDetail saleDetail in saleDetails)
                {
                    Product product = ProductServices.GetProduct(saleDetail.ProductCode);
                    product.QuantityStock -= saleDetail.NbProduct;
                    ProductServices.UpdateProduct(product);
                }
            }
        }
        public static int QuantitySoldOfAProduct(string Code,DateTime start, DateTime end)
        {
            using(MyPopupStoreDBContext db= new())
            {
                return db.SaleDetails.Where(e=> e.ProductCode==Code).Sum(e => e.NbProduct) ?? default(int);
            }
        }
        public static decimal GetTotal()
        {
            using (MyPopupStoreDBContext db = new())
            {
                return db.SaleDetails.Sum(e => e.Price*e.NbProduct)??default(decimal);
            }
        }
    }
}
