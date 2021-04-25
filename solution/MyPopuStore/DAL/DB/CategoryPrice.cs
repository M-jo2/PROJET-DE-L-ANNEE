using System;
using System.Collections.Generic;

#nullable disable

namespace MyPopuStore.DAL.DB
{
    public partial class CategoryPrice
    {
        public CategoryPrice()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryPriceId { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
