using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Application.Dtos.Carts
{
	public class CartLineDto
	{
		public string ProductName { get; set; }
		public int ProductId { get; set; }
		public int CartId { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
	}
}
