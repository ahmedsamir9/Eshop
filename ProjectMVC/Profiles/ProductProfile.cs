using AutoMapper;

namespace ProjectMVC.Profiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, ViewModel.ProductVM>();
        }
    }
}
