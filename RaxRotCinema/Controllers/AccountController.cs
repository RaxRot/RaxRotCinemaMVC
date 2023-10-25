using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Models.ViewModels;
using RaxRotCinema.Helper;

namespace RaxRotCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerVM.UserName,
                    Email = registerVM.Email

                };

                var identityResult =
                     await _userManager.CreateAsync(identityUser, registerVM.Password);

                if (identityResult.Succeeded)
                {
                    //assign the user role
                    var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");

                    if (roleIdentityResult.Succeeded)
                    {
                        TempData[TagManager.ToastrSuccess] = "Registered";

                        return RedirectToAction("Login");
                    }
                }
            }

            TempData[TagManager.ToastrError] = "Error";
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginVM
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(loginVM.UserName,
                 loginVM.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginVM.ReturnUrl))
                {
                    return Redirect(loginVM.ReturnUrl);
                }

                TempData[TagManager.ToastrSuccess] = "Login";

                return RedirectToAction("Index", "Home");
            }

            TempData[TagManager.ToastrError] = "Error";
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData[TagManager.ToastrWarning] = "Logout";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
