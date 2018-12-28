using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ModernShopping.Presentation.Controllers.Api
{
    public class ImagesController : ApiBaseController
    {
        private readonly IHostingEnvironment _environment;

        public ImagesController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadImage(IFormFile imageUpload)
        {

            string imageFolderName = "product-images";
            string wwwRoot = _environment.WebRootPath;


            string imageFolderPath = Path.Combine(wwwRoot, imageFolderName);

            if (imageUpload.Length > 0)
            {
                var fileExtension = Path.GetExtension(imageUpload.FileName);
                var newFileName = $"{Guid.NewGuid()}{fileExtension}";
                var fullPath = Path.Combine(imageFolderPath, newFileName);

                var image = Image.Load(imageUpload.OpenReadStream());
                image.Mutate(context => context.Resize(800, 0));
                image.Save(fullPath);
            }


            return Ok();

        }
    }
}
