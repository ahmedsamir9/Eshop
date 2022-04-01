using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;
using ProjectMVC.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public ShopDBContext Context { get; }

        public AccountController(UserManager<IdentityUser> _UserManager,
            SignInManager<IdentityUser> _SignInManager, RoleManager<IdentityRole> _RoleManager, ShopDBContext context)
        {
            UserManager = _UserManager;
            SignInManager = _SignInManager;
            RoleManager = _RoleManager;
            Context = context;
        }
        public IActionResult SignUp(string ReturnUrl = "~/Home/Index")
        {
         
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(Client account,
            string ReturnUrl = "~/Home/Index")
        {
            if(ModelState.IsValid)
            {
                //map from vm to model
                IdentityUser user = new IdentityUser();
                user.UserName = account.Name;
                user.Email = account.Email;
                // A7TH AL PASSWORED KDA 3L4AN E3MLO HASH  
                //save in db
                IdentityResult result = await UserManager.CreateAsync(user, account.Password);
                if (result.Succeeded)
                {
                    //ha3mel obect mn user 
                    //Client  client = new Client();
                    //ba3d kda asawe al id bta3o by al id bta3 IdentityUser
                    account.Id=user.Id;
                    // hna hdef fe dbset bta3t al user
                    Context.clients.Add(account);
                    // ba3d kda adef savechange
                    Context.SaveChanges();
                    // add clien role 
                  await UserManager.AddToRoleAsync(user, "Client");
                    //create cookie for registeration
                    await SignInManager.SignInAsync(user, account.RememberMe);
                    return LocalRedirect(ReturnUrl);
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(account);
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl = "~/Home/Index")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        public async Task<IActionResult> Login(LoginVM account,
           string ReturnUrl = "~/Home/Index")
        {
            if (ModelState.IsValid)
            {
                //find user in db
                IdentityUser user = await UserManager.FindByNameAsync(account.Name);
                if (user != null)
                {
                    //create cookie for login if user.password == account.Password
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await SignInManager.PasswordSignInAsync(user, account.Password, account.RememberMe, false);
                    if (result.Succeeded)
                        return LocalRedirect(ReturnUrl);
                    //present error to client that is resulted from error in password
                    ModelState.AddModelError(string.Empty, "invalid password");
                }
                ModelState.AddModelError(string.Empty, "invalid account");
            }
            return View(account);
        }
        [HttpPost]
        public IActionResult ExternalLogin(string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            string provider = "Google";
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        [TempData]
        public string ErrorMessage { get; set; }
        [HttpGet]
 
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(ExternalLogin));
            }
            var info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(ExternalLogin));
            }
            // Sign in the user with this external login provider if the user already has a login.
            var result = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
                //_logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                //return RedirectToAction(nameof(returnUrl));
            }
            else
            {
                string email = string.Empty;
                string firstName = string.Empty;
                string lastName = string.Empty;
                string profileImage = string.Empty;
                //get google login user infromation like that.
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
                {
                    firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                }
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
                {
                    lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
                }
                var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                var result2 = await UserManager.CreateAsync(user);
                if (result2.Succeeded)
                {
                    result2 = await UserManager.AddLoginAsync(user, info);
                    if (result2.Succeeded)
                    {
                        //do somethng here
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View("Login");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Exsist(string Email)
        {
            IdentityUser user = await UserManager.FindByEmailAsync(Email);
            if (user == null)
                return Json(true);
            return Json(false);
        }

        [HttpGet]
        public IActionResult SignUpAdmin(string ReturnUrl = "~/")
        {
                ViewBag.ReturnUrl = ReturnUrl;
                 return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAdmin(Client account,
            string ReturnUrl = "~/Home/Index")
        {
            if (ModelState.IsValid== true)
            {
                //map from vm to model
                IdentityUser user = new IdentityUser();
                user.UserName = account.Name;
                user.Email = account.Email;
                // A7TH AL PASSWORED KDA 3L4AN E3MLO HASH  
                //save in db
                IdentityResult result = await UserManager.CreateAsync(user, account.Password);
                if (result.Succeeded)
                {
                    //ha3mel obect mn user 
                    //Client  client = new Client();
                    //ba3d kda asawe al id bta3o by al id bta3 IdentityUser
                    account.Id = user.Id;
                    // hna hdef fe dbset bta3t al user
                    Context.clients.Add(account);
                    // ba3d kda adef savechange
                    Context.SaveChanges();
                    // add to admin role 
                     await UserManager.AddToRoleAsync(user, "Admin");
                    //create cookie for registeration
                    await SignInManager.SignInAsync(user, account.RememberMe);
                    return LocalRedirect(ReturnUrl);
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(account);
        }

        
    }
}
