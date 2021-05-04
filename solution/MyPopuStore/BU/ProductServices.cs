using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    using DAL.DB;
    using Microsoft.EntityFrameworkCore;

    class ProductServices
    {
        public static void AddProduct(string code,string label, CategoryPrice catPrice, int quantityStock, string picturePath = null) 
        {

            Product product = new()
            {
                QuantityStock = quantityStock,
                Code = code,
                Label = label,
                CategoryPriceId = catPrice.CategoryPriceId,
                Picture = picturePath
            };
            

            using(MyPopupStoreDBContext db = new())
            {
                try
                {
                    db.Add(product);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    throw new Exception("Produit invalide", e);
                }
                
            }
        }
        public static bool ProductIsUsed(string code)
        {
            using (MyPopupStoreDBContext db = new())
            {
                return db.SaleDetails.Where(e => e.ProductCode == code).Any();
            }
        }
        public static bool ProductExist(string code)
        {
            using (MyPopupStoreDBContext db = new())
            {
                return db.Products.Where(e => e.Code == code).Any();
            }
        }

        public static void RemoveProduct(string code)
        {
            using (MyPopupStoreDBContext db = new())
            {
                if(ProductServices.ProductIsUsed(code))
                    throw new Exception("Impossible de supprimer un produit enregistré dans au moins une vente.");

                db.Products.Remove(GetProduct(code));
                db.SaveChanges();
            }
        }
        public static void UpdateProduct(Product product)
        {
            using (MyPopupStoreDBContext db = new())
            {
                db.Products.Update(product);
                db.SaveChanges();
            }
        }
        public static Product GetProduct(string code)
        {
            using (MyPopupStoreDBContext db = new MyPopupStoreDBContext())
            {
                return db.Products.Find(code);
            }
        }
        public static List<Product> GetAllProduct(string codeStartWith="") 
        {
            using (MyPopupStoreDBContext db = new MyPopupStoreDBContext())
            {
                List < Product > products = db.Products.Where(e => e.Code.StartsWith(codeStartWith)).ToList();
                return products;
            }
        }
    }
}
