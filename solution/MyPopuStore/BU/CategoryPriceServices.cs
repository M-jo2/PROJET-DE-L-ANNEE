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
        public static void add(string color,decimal price) {
            using (DB.MyPopupStoreDBContext db = new DB.MyPopupStoreDBContext())
            {
                db.Add(new CategoryPrice() { Color = color, Price = price });
                db.SaveChanges();
            }
        }
        public static void delete(CategoryPrice categoryPrice) {
            using (DB.MyPopupStoreDBContext db = new DB.MyPopupStoreDBContext())
            {
                db.Remove(categoryPrice);
                db.SaveChanges();
            }
        }

        public static CategoryPrice GetPrice(int ID)
        {
            CategoryPrice category = null;
            using (MyPopupStoreDBContext db = new())
            {
                category = db.CategoryPrices.Find(ID);
            }
            return category==null ? new CategoryPrice() { Color = "#fff", Price = 0 } : category;
        }

        public static List<CategoryPrice> GetAllPrice()
        {
            using (DB.MyPopupStoreDBContext db = new DB.MyPopupStoreDBContext())
            {
                return db.CategoryPrices.ToList();
            }
        }
    }
}
