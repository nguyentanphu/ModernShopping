using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public async Task<ActionResult> UploadImage(IFormFile imageUpload)
        {
            if (imageUpload.Length == 0)
            {
                throw new EmptyFileException();
            }

            var client = new HttpClient();
            var token = this.HttpContext.Request.Headers["Authorization"][0].Replace("Bearer", string.Empty);
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5000");
            if (disco.IsError) throw new Exception(disco.Error);

            var clientUserInfo = new HttpClient();

            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = token
            });

            var imagePath = _imageService.GenerateUniqueImagePath(_environment.WebRootPath, imageUpload.FileName);
            var loadedImage = _imageService.Load(imageUpload.OpenReadStream());
            _imageService.Resize(loadedImage, ApplicationConst.ImageMaxWidth);
            var imageId = await _imageService.Save(loadedImage, imagePath);

            return Ok(new { imageId });

        }
    }
}
