using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Application.Dtos.Products
{
	public class ProductForCreationDto
	{
		public string ProductName { get; set; }
		public LabelValueObject Supplier { get; set; }
		public LabelValueObject Category { get; set; }
		public string QuantityPerUnit { get; set; }
		public decimal UnitPrice { get; set; }
		public short UnitsInStock { get; set; }
	    public short UnitsOnOrder { get; set; }
        public int ImageId { get; set; }
    }
}
