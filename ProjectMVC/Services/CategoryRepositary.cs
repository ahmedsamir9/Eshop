using ProjectMVC.Models;

namespace ProjectMVC.Services
{
    public class CategoryRepositary : GenericRepository<Category>
    {
        public CategoryRepositary(ShopDBContext context) : base(context) { }
    }
}
