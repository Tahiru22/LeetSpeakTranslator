using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeetSpeakTranslator.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Translation");
            }
            
            var errors = result.Errors.Select(e => e.Description);
            ViewBag.Errors = errors;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(UserController.Login), "User");
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Translation");
            }
           
            var errors = new List<string>();
            if (result.IsLockedOut)
            {
                errors.Add("User account is locked out.");
            }
            else if (result.IsNotAllowed)
            {
                errors.Add("User is not allowed to sign in.");
            }
            else
            {
                errors.Add("Invalid login attempt.");
            }
            ViewBag.Errors = errors;
            return View();
        }



    }
}
