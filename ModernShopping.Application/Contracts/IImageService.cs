using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ModernShopping.Application.Contracts
{
    public interface IImageService
    {
        Image<Rgba32> Load(Stream stream);
        void Resize(Image<Rgba32> image, int width, int height = 0);
        void Save(Image<Rgba32> image, string path);
        string GenerateUniqueImagePath(string wwwRootPath, string fileName);
    }
}