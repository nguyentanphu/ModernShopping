using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ModernShopping.Application.Contracts;
using ModernShopping.Persistence;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ModernShopping.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly NorthwindContext _context;

        public ImageService(NorthwindContext context)
        {
            _context = context;
        }

        public Image<Rgba32> Load(Stream stream)
        {
            return Image.Load(stream);
        }

        public async Task<int> Save(Image<Rgba32> image, string path)
        {
            image.Save(path);

            var newImage = new Persistence.Entities.Image
            {
                FileName = Path.GetFileName(path)
            };
            _context.Images.Add(newImage);
            await _context.SaveChangesAsync();

            return newImage.ImageId;
        }

        // when height or width = 0, the resize image will scale proportionally 
        public void Resize(Image<Rgba32> image, int width, int height = 0)
        {
            image.Mutate(context => context.Resize(width, height));
        }

        public string GenerateUniqueImagePath(string wwwRootPath, string fileName)
        {
            string imageFolderName = "product-images";
            string imageFolderPath = Path.Combine(wwwRootPath, imageFolderName);
            var fileExtension = Path.GetExtension(fileName);

            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            return Path.Combine(imageFolderPath, newFileName);
        }
    }
}
