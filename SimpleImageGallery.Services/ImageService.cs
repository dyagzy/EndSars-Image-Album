


using Microsoft.EntityFrameworkCore;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleImageGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly SimpleImageGalleryDbContext _ctx;
       
        public ImageService(SimpleImageGalleryDbContext ctx)
        {
            _ctx = ctx;

          
        }

        //Declare properties for the Cloudinary Api
        





        public IEnumerable<GalleryImage> GetAll()
        {
            return _ctx.GalleryImages.Include(x => x.Tags);
        }

        public GalleryImage GetById(int id)
        {
            return GetAll().Where(img => img.Id == id).First();

            //return _ctx.GalleryImages.Find(id);
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll().Where(img => img.Tags.Any(t => t.Description == tag));
        }

        
    }
}
