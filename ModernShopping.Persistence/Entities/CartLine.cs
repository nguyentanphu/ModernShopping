using System;
using System.Collections.Generic;

namespace ModernShopping.Persistence.Entities
{
    public partial class CartLine
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }

        public Product Product { get; set; }
    }
}
