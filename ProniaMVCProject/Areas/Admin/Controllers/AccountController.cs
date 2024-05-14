using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProniaMVCProject.Core.Models;
using ProniaMVCProject.ViewModels;

namespace ProniaMVCProject.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManagar;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManagar)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManagar = roleManagar;
        }
        public IActionResult Login() 
        {
            return View();
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser()
            {
                FullName = "Dua Lipa",
                UserName = "SuperAdmin"
            };

            await _userManager.CreateAsync(admin, "Admin123@");
            await _userManager.AddToRoleAsync(admin, "SuperAdmin");

            return Ok("Admin Yarandi");
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole identityRole = new IdentityRole("SuperAdmin");
            IdentityRole identityRole1 = new IdentityRole("Admin");
            IdentityRole identityRole2 = new IdentityRole("Member");
            await _roleManagar.CreateAsync(identityRole);
            await _roleManagar.CreateAsync(identityRole1);
            await _roleManagar.CreateAsync(identityRole2);

            return Ok("Rollar yarandi!");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser user = await _userManager.FindByNameAsync(adminLoginVm.Username);

            if(user == null)
            {
                ModelState.AddModelError("", "Username or password is not valid"); 
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVm.Password, adminLoginVm.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is not valid");
                return View();
            }


            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
