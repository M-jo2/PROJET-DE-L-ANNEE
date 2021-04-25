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
        public static void add(string color,decimal Price) {
            using (DB.MyPopupStoreDBContext db = new DB.MyPopupStoreDBContext())
            {
                
            }
        }
        public static decimal getPrice(int CategoryPriceID) { return 0;  }
    }
}
