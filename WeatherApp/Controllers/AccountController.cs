using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherApp.Models.ViewModels;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        public IActionResult LoginPage(string returnUrl)
        {
            //Method to create initial accounts and roles
            //await CreateRoleAndAccounts();
            if (User.Identity.IsAuthenticated)
                return Redirect("/Home/HomePage");
            else
                return View();
        }
        [Authorize]
        public async Task<RedirectResult> Logout()
        {
            await _identityService.LogoutAsync();
            return Redirect("LoginPage");
        }
        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LoginPage(LoginVM loginData, string returnUrl)
        {
            bool isNullOrEmpty = AdditionalServices.CheckIsNullOrEmpty(loginData);
            if (!isNullOrEmpty)
            {
                bool result = await _identityService.LoginAsync(loginData);
                if (result)
                {
                    bool isAdmin = await _identityService.CheckIsAdminAsync(loginData.Username);
                    if (isAdmin)
                    {
                        if (String.IsNullOrWhiteSpace(returnUrl))
                            returnUrl = "/Home/AdministratorPanel";

                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(returnUrl))
                            returnUrl = "/Home/UserPanel";

                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View(loginData);
                }
            }
            else
            {
                ModelState.AddModelError("", "You must insert username and password");
                return View(loginData);
            }

        }
        public async Task CreateRoleAndAccounts()
        {
        List<string> _roles = new List<string>
        {"StandardUser","Administrator"};

        List<LoginVM> loginData = new List<LoginVM>
        {
            new LoginVM
            {
                Username = "user",
                Password = "Aa123456."
            },
            new LoginVM
            {
                Username = "admin",
                Password = "Aa123456."
            }
        };
            await _identityService.CreateRolesAsync(_roles);
            await _identityService.SetStandardUserAccountAsync(loginData[0]);
            await _identityService.SetAdminAccountAsync(loginData[1]);
        }
    }
}
