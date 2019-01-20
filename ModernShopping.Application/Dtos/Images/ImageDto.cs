using System;
using System.Collections.Generic;
using System.Text;
using ModernShopping.Application.Common;

namespace ModernShopping.Application.Dtos.Images
{
	public class ImageDto
	{
		public int ImageId { get; set; }
		public string FileName { get; set; }
		public string ImageUrl => $"{ApplicationConst.BaseProductImageUrl}{FileName}";
	}
}
