using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Application.Dtos.Carts
{
	public class CartDto
	{
		public IEnumerable<CartLineDto> CartLines = new List<CartLineDto>();
	}
}
