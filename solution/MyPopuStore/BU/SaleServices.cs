using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    using DAL.DB;
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

        public static void NewSale(List<SaleDetail> saleDetails,bool paymentType,bool decrementQuantityStock=true)
        {
            using(MyPopupStoreDBContext db = new())
            {
                
                Sale sale = new()
                {
                    PaymentType = paymentType,
                    Date = DateTime.Now
                };
                sale.SaleDetails = saleDetails;

                
                db.Sales.Add(sale);

                if (decrementQuantityStock)
                {
                    foreach(SaleDetail saleDetail in saleDetails)
                    {
                        Product product = ProductServices.GetProduct(saleDetail.ProductCode);
                        product.QuantityStock -= saleDetail.NbProduct;

                        if (product.QuantityStock < 0) throw new Exception("Pas assez de '" + product.Label+"' dans le stock");
                        db.Products.Update(product);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
