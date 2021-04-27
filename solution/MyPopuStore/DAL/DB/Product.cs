using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MyPopuStore.DAL.DB
{
    public partial class Product
    {
        public Product()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        public string Code { get; set; }
        public string Label { get; set; }
        public int? CategoryPriceId { get; set; }
        public int? QuantityStock { get; set; }
        public string Picture { get; set; }

        public virtual CategoryPrice CategoryPrice { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
