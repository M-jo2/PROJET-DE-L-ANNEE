using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Product_Page
{
    class ProductUI
    {
        private CategoryPrice categoryPrice;
        private Product product;

        public CategoryPrice CategoryPrice { get => categoryPrice; set => categoryPrice = value; }
        public Product Product { get => product; set => product = value; }

    }
}
