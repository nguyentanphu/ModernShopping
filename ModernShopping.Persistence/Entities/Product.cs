using System;
using System.Collections.Generic;

namespace ModernShopping.Persistence.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
			ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; private set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        //public bool Discontinued { get; set; }    Using EF shadow property instead

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
	    public ICollection<ProductImage> ProductImages { get; set; }
	}
}
