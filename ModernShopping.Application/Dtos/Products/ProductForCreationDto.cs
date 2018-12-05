using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Application.Dtos.Products
{
	public class ProductForCreationDto
	{
		public string ProductName { get; set; }
		public int? SupplierId { get; set; }
		public int? CategoryId { get; set; }
		public string QuantityPerUnit { get; set; }
		public decimal? UnitPrice { get; set; }
		public short? UnitsInStock { get; set; }
	}
}
