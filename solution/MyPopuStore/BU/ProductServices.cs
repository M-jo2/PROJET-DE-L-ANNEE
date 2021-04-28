using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    using DAL.DB;
    class ProductServices
    {
        public static void addProduct(string code,string label, CategoryPrice catPrice, int quantityStock, string picturePath = null) 
        {
            Product product = new Product()
            {
                QuantityStock = quantityStock,
                Code = code,
                Label = label,
                CategoryPriceId = catPrice.CategoryPriceId,
                Picture = picturePath
            };

            using(MyPopupStoreDBContext db = new MyPopupStoreDBContext())
            {
                db.Add(product);
                db.SaveChanges();
            }
        }


        public static void removeProduct(string code) { }
        public static void setCategoryPriceOfProduct(string code, CategoryPrice catPrice) { }
        public static void setPictureOfProduct(string code, string picturePath) { }
        public static void setStockProduct(string code, int nbProducts) { }
        public static void substractStockProduct(string code, int nbProducts) { }
        public static Product getProduct(string code)
        {
            using (MyPopupStoreDBContext db = new MyPopupStoreDBContext())
            {
                return db.Products.Find(code);
            }
        }
        public static List<Product> getAllProduct() 
        {
            using (MyPopupStoreDBContext db = new MyPopupStoreDBContext())
            {
                List < Product > products = db.Products.ToList();
                return products;
            }
        }
    }
}
