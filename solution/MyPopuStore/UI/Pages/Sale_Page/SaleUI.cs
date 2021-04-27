using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Sale_Page
{
    class SaleUI
    {
        public Sale Sale { get; set; }
        public Decimal Total { get; set; }
        public int QuantityOfProduct { get; set; }
    }
}
