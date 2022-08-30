using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.ViewModel.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<CustomersController> logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController
            (ILogger<CustomersController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context
            )

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var signedUser = await _userManager.FindByEmailAsync(model.Password);
                    
                   
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                    };
                    var result = await _userManager.CreateAsync((ApplicationUser)user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync((ApplicationUser)user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }
                return View(model);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var signedUser = await _userManager.FindByEmailAsync(user.Email);
                    if (signedUser == null)
                    {
                        ViewData["msg"] = "Invalid Email or Password";
                        return View(user);
                    }
                    var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, user.Password, user.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }

                return View(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                ViewData["msg"] = "Invalid Email or Password";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
