using ImageGallery.Infrastructures;
using ImageGallery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Controllers
{
    public class ImageController : Controller
    {
        private readonly ICloudinaryImageUpload _imageUpload;

        public ImageController(ICloudinaryImageUpload imageUpload)
        {
            _imageUpload = imageUpload;
        }

        [Authorize]
        public IActionResult Upload()
        {
            var model = new UploadImageModel();

            return View(model);
        }

        [HttpPost]

        public async Task< IActionResult> UploadNewImage(UploadImageModel model)
        {

            await _imageUpload.UploadPicture(model);
            return RedirectToAction("Index", "Gallery");
            
        }
    }
}
