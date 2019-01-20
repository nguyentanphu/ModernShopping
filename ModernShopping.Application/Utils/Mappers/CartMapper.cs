using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ModernShopping.Application.Dtos.Carts;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.Application.Utils.Mappers
{
	public static class CartMapper
	{
		public static Expression<Func<Cart, CartDto>> EntityToDtoExpression =>
			c => new CartDto
			{
				CartLines = c.CartLines.Select(CartLineEntityToDto)
			};

		public static Func<Cart, CartDto> EntityToDtoFunc = EntityToDtoExpression.Compile();

		public static Func<CartLineDto, CartLine> CartLineDtoToEntityFunc =>
			c => new CartLine
			{
				CartId = c.CartId,
				ProductId = c.ProductId,
				Quantity = c.Quantity,
				UnitPrice = c.UnitPrice
			};

		public static Func<CartLine, CartLineDto> CartLineEntityToDto =>
			cl => new CartLineDto
			{
				CartId = cl.CartId,
				ProductName = cl.Product?.ProductName,
				ProductId = cl.ProductId,
				Quantity = cl.Quantity,
				UnitPrice = cl.UnitPrice
			};
	}
}
