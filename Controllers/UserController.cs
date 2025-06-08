using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace RecipeSharingSystem.Web.Controllers
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Register(string email, string password)
{
    if (password.Length < 6)
    {
        TempData["Message"] = "Need at least 6 characters!";
        return RedirectToAction("Register");
    }

    var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
    var result = await _userManager.CreateAsync(user, password);
    if (result.Succeeded)
    {
        TempData["Message"] = "Registration successful. Please log in.";
        return RedirectToAction("Login");
    }
    foreach (var error in result.Errors)
    {
        ModelState.AddModelError("", error.Description);
    }
    return View();
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}