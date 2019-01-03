using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernShopping.Application.Common;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Exceptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ModernShopping.Presentation.Controllers.Api
{
    public class ImagesController : ApiBaseController
    {
        private readonly IHostingEnvironment _environment;
        private readonly IImageService _imageService;

        public ImagesController(IHostingEnvironment environment, IImageService imageService)
        {
            _environment = environment;
            _imageService = imageService;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadImage(IFormFile imageUpload)
        {
            if (imageUpload.Length == 0)
            {
                throw new EmptyFileException();
            }

            var imagePath = _imageService.GenerateUniqueImagePath(_environment.WebRootPath, imageUpload.FileName);
            var loadedImage = _imageService.Load(imageUpload.OpenReadStream());
            _imageService.Resize(loadedImage, ApplicationConst.ImageMaxWidth);
            _imageService.Save(loadedImage, imagePath);

            return Ok();

        }
    }
}
