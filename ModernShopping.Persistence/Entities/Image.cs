using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Persistence.Entities
{
	public class Image
	{
		public Image()
		{
			ProductImages = new HashSet<ProductImage>();
		}
		public int ImageId { get; set; }
		public string FileName { get; set; }

		public ICollection<ProductImage> ProductImages { get; set; }
	}
}
