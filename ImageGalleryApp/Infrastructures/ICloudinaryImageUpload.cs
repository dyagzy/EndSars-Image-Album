using ImageGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Infrastructures
{
    public interface ICloudinaryImageUpload
    {
        Task<string> UploadPicture(UploadImageModel model);
    }
}
