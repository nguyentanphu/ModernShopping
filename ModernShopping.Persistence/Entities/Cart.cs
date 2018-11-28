using System;
using System.Collections.Generic;

namespace ModernShopping.Persistence.Entities
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public int CartId { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
