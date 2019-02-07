using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Application.Dtos.Carts
{
	public class CartLineForCreationDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
	}
}
