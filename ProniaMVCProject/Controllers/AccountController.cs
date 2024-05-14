using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProniaMVCProject.Core.Models;
using ProniaMVCProject.ViewModels;

namespace ProniaMVCProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManagar)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVm memberRegisterVm)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser appUser = null;

            appUser = await _userManager.FindByNameAsync(memberRegisterVm.Username);

            if(appUser is not null)
            {
                ModelState.AddModelError("Username", "Username already exist");
                return View();
            }

            appUser = await _userManager.FindByEmailAsync(memberRegisterVm.Email);

            if(appUser is not null)
            {
                ModelState.AddModelError("", "Email already exist");
                return View();
            }

            appUser = new AppUser()
            {
                UserName = memberRegisterVm.Username,
                FullName = memberRegisterVm.FullName,
                Email = memberRegisterVm.Email,

            };

            var result = await _userManager.CreateAsync(appUser, memberRegisterVm.Password);    

            if(!result.Succeeded)
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(appUser, "Member");
            return RedirectToAction("Login"); 
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberRegisterVm memberRegisterVm)
        {

            AppUser user = await _userManager.FindByEmailAsync(memberRegisterVm.Email);


            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is not valid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, memberRegisterVm.Password, memberRegisterVm.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is not valid");
                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
