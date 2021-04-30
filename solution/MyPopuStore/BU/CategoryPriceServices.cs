using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    using DB = DAL.DB;
    class CategoryPriceServices
    {
        public static void Add(string color,decimal price) {
            using (DB.MyPopupStoreDBContext db = new())
            {
                db.Add(new CategoryPrice() { Color = color, Price = price });
                db.SaveChanges();
            }
        }
        public static void Delete(CategoryPrice categoryPrice) {
            if (CategoryPriceServices.CategoryPriceIsUsed(categoryPrice.CategoryPriceId))
            {
                throw new Exception("Impossible de supprimer un prix appliqué à au moins un produit.");
            }

            using (DB.MyPopupStoreDBContext db = new())
            {
                
                db.Remove(categoryPrice);
                db.SaveChanges();
            }
        }

        public static CategoryPrice GetPrice(int ID)
        {
            using (MyPopupStoreDBContext db = new())
            {
                CategoryPrice category = db.CategoryPrices.Find(ID);
                if (category == null) throw new Exception("Catégorie de prix inexistant");
                return category;
            }
        }

        public static List<CategoryPrice> GetAllPrice()
        {
            using (DB.MyPopupStoreDBContext db = new())
            {
                return db.CategoryPrices.ToList();
            }
        }

        public static bool CategoryPriceIsUsed(int catId)
        {
            using (DB.MyPopupStoreDBContext db = new())
            {
                return db.Products.Where(e => e.CategoryPriceId == catId).Any();
            }
        }
    }
}
