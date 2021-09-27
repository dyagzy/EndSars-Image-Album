using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class UploadImageModel
    {
        public string Title { get; set; }
        public IFormFile UploadImage { get; set; }
        public string Tags { get; set; }
    }
}
