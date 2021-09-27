using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ImageGallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Infrastructures
{
    public class CloudinaryImageUpload : ICloudinaryImageUpload
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SimpleImageGalleryDbContext _ctx;

        public CloudinaryImageUpload(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, 
            SimpleImageGalleryDbContext ctx)
        {


            this.ApiKey = configuration["Cloudinary:ApiKey"];
            this.ApiSecret = configuration["Cloudinary:ApiSecret"];
            this.Cloud = configuration["Cloudinary:Cloud"];
            this.Account = new Account { ApiKey = this.ApiKey, ApiSecret = this.ApiSecret, Cloud = this.Cloud };
            this.configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _ctx = ctx;
        }
        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private string Cloud { get; set; }
        private Account Account { get; set; }
        public async Task<string> UploadPicture(UploadImageModel model)
        {
            var cloudinary = new Cloudinary(Account);
            cloudinary.Api.Secure = true;


            //string uploads = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
            //string path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "endSars.jpg");
            ////Create directory if it doesn't exist 
            //Directory.CreateDirectory(path);
           
            //    if (model.UploadImage.Length > 0)
            //    {
            //        string filePath = Path.Combine(path, model.UploadImage.FileName);

            //        using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            //        {
            //            model.UploadImage.CopyTo(fileStream);
            //        }
            //    }
            

            //reads the Image in the IFormFile into a string
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                model.UploadImage.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);

            var prefix = @"data:image/png;base64,";
            var imagePath = prefix + base64;
            //var streamString = new MemoryStream(Encoding.UTF8.GetBytes(base64));


            //byte[] data = Convert.FromBase64String(base64);
            //string decodedString = Encoding.UTF8.GetString(data);


            //var reader = new StreamReader(model.UploadImage.OpenReadStream());
            //var @imagePath = reader.ReadToEnd().ToString();


            var uploadParams = new ImageUploadParams()

            {
                File = new FileDescription(imagePath),
                Folder = "EndSars/img"
            };

            var uploadResult = await cloudinary.UploadAsync(@uploadParams);
            //reader.Close();

            
            // adds the new image to be uploaded to the databse
            var image = new GalleryImage()
            {
                Title = model.Title,
                Created = DateTime.Now,
                Url = uploadResult.Url.AbsoluteUri,
                Tags = ParseTags(model.Tags)

            };
            _ctx.Add(image);
            await _ctx.SaveChangesAsync();
            
            return uploadResult.SecureUrl.AbsoluteUri;
        }

      
        

        public List<ImageTag> ParseTags(string tags)
        {
            return tags.Split(",").Select(tag => new ImageTag
            {
                Description = tag
            }).ToList();

        }
    }
}
