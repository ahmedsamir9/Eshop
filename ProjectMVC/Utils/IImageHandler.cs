using ProjectMVC.Models;

namespace ProjectMVC.Utils
{
    public interface IImageHandler
    {

        public string UploadImage(Product product);
        public void RemoveImage(string imgPath);


    }
}
