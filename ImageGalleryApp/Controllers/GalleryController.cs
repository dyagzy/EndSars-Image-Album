using ImageGallery.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImageService _iIMageService;

        public GalleryController(IImageService iIMageService)
        {
            _iIMageService = iIMageService;
        }
        public IActionResult Index()
        {
            var imageList = _iIMageService.GetAll();


                var model = new GalleryIndexModel()
                {
                    Images = imageList,
                    SearchQuery = ""
                };
                return View(model);
            
            
        }
        public IActionResult Detail(int id)
        {
            var image = _iIMageService.GetById(id);
            var model = new GalleryDetailModel()
            {
                Id = image.Id,
                Created = image.Created,
                Url = image.Url,
                Title = image.Title,
                Tags = image.Tags.Select(x => x.Description).ToList()
             };

            return View(model);
        

        }
    
    }
}
