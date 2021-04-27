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
                total = db.SaleDetails.Where(e => e.SaleId == SaleID).Sum(e => e.Price);
                if (total == null) throw new Exception("Quantité de produit null");
                return (decimal)total;
            }
        }
        public static List<Product> getProductsOneSale(int SaleID) { return null; }
    }
}
