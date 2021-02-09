using System;
using System.Collections.Generic;

#nullable disable

namespace MyPopuStore.DAL.DBd
{
    public partial class SaleDetail
    {
        public int SaleDetailsId { get; set; }
        public string ProductCode { get; set; }
        public int SaleId { get; set; }
        public int? NbProduct { get; set; }
        public decimal Price { get; set; }

        public virtual Product ProductCodeNavigation { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
