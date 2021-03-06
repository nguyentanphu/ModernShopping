﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Persistence.Entities
{
	public class ProductImage
	{
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public int ImageId { get; set; }
		public Image Image { get; set; }
	}
}
