using LeaveManagement.Mvc.Contracts;
using LeaveManagement.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm login, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");
                var isLoggedIn =await _authenticationService.Authenticate(login.UserName, login.Password);
                if (isLoggedIn)
                    return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("","Login attemped is failed");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterVm register)
        {
            if (ModelState.IsValid)
            {
               var returnUrl = Url.Content("~/");
               var isCreated =await _authenticationService.Register(register);
                if(isCreated)
                    return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("", "Login attemped is failed");
            return View(register);
        }

        public async Task<ActionResult> Logout(string returnUrl)
        { 
            returnUrl ??= Url.Content("~/");
            await _authenticationService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}
