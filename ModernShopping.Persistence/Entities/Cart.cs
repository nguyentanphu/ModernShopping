using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernShopping.Persistence.Entities
{
    public partial class Cart
    {
        public Cart()
        {
            CartLines = new HashSet<CartLine>();
        }

        public int CartId { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedDate { get; private set; }

        public ICollection<CartLine> CartLines { get; set; }

        private CartLine GetCurrentCartLine(CartLine cartLine)
        {
            if (cartLine == null) throw new ArgumentException("cartLine cannot be null", nameof(cartLine));

            return CartLines.FirstOrDefault(l => l.ProductId == cartLine.ProductId);
        }
        public CartLine AddCartLine(CartLine cartLine)
        {
            var existingLine = GetCurrentCartLine(cartLine);

            if (existingLine == null)
                CartLines.Add(cartLine);
            else
            {
                existingLine.Quantity = cartLine.Quantity;
                existingLine.UnitPrice = cartLine.UnitPrice;
            }

            return cartLine;
        }

    }
}
