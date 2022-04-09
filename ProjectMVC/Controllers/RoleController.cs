using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectMVC.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public RoleController(RoleManager<IdentityRole> _RoleManager)
        {
            RoleManager = _RoleManager;
        }
        public IActionResult GetAll()
        {
             var roleList = RoleManager.Roles.Select(x => x.Name).ToList();
            return View(roleList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM role)
        {
            if (ModelState.IsValid== true)
            {
                //map from vm to model
                IdentityRole myRole = new IdentityRole() { Name = role.Name };
                //save in db
                //kda tmam
                IdentityResult result = await RoleManager.CreateAsync(myRole);
                if (result.Succeeded)
                    //return RedirectToAction("GetAll");
                    return View();
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(role);
        }
        public async Task<IActionResult> Delete(string name)
        {
            IdentityRole role = await RoleManager.FindByNameAsync(name);
            try
            {
                await RoleManager.DeleteAsync(role);
                return RedirectToAction("GetAll");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "This Item is in use");
            }
            return View("GetAll");
        }
        public async Task<IActionResult> Exsist(string Name)
        {
            IdentityRole role = await RoleManager.FindByNameAsync(Name);
            if (role == null)
                return Json(true);
            return Json(false);
        }
    }
}
