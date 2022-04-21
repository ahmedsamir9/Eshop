using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ProjectMVC.Controllers
{
    [Authorize(Roles = "Client")]
    public class OrdersController : Controller
    {


        private static string clientID = "";
        public UserManager<IdentityUser> UserManager { get; }

        private readonly GenericRepository<Order> repo;
        private readonly GenericRepository<OrderProduct> repoOrderProducts;

        public OrdersController(GenericRepository<Order> _repo, GenericRepository<OrderProduct> _repoOrderProducts, UserManager<IdentityUser> _UserManager)
        {
            repo = _repo;
            repoOrderProducts = _repoOrderProducts;
            UserManager = _UserManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            clientID = user.Id;

            Expression<Func<Order, bool>> predicate = o => o.ClientId == clientID;
            return View(repo.Find(predicate));
        }

        // GET: Orders/Details/5
        public IActionResult Details(int id)
        {
            Expression<Func<OrderProduct, bool>> predicate = op => op.OrderId == id;
            return View(repoOrderProducts.Find(predicate));
        }
    }
}
