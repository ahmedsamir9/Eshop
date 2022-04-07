using Microsoft.AspNetCore.Hosting;
using ProjectMVC.Models;
using System;
using System.IO;

namespace ProjectMVC.Utils
{
    public class ImageHandler : IImageHandler
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public ImageHandler(IWebHostEnvironment webhost)
        {
            webHostEnvironment = webhost;

        }
        public void RemoveImage(string imgPath)
        {
            string image = Path.Combine(webHostEnvironment.WebRootPath, "images", imgPath);
            FileInfo fi = new FileInfo(image);
            if (fi != null)
            {
                System.IO.File.Delete(image);
                fi.Delete();
            }
        }

        public string UploadImage(Product product)
        {
            string uniqueFileName = null;

            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);

                }

            }

            return uniqueFileName;
        }
    }
}
