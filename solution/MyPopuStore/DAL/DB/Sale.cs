using System;
using System.Collections.Generic;

#nullable disable

namespace MyPopuStore.DAL.DBd
{
    public partial class Sale
    {
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        public int SaleId { get; set; }
        public bool PaymentType { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
