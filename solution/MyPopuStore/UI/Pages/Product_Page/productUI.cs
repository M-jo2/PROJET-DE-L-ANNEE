using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Product_Page
{
    class ProductUI
    {
        private CategoryPrice categoryPrice;
        private DAL.DB.Product product;

        public CategoryPrice CategoryPrice { get => categoryPrice; set => categoryPrice = value; }
        public DAL.DB.Product Product { get => product; set => product = value; }
    }
}
