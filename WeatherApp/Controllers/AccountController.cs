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

        public readonly IReadingsService _readingService;

        public AccountController(IIdentityService identityService, IReadingsService readingsService)
        {
            _identityService = identityService;
            _readingService = readingsService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginPage(string returnUrl, LoginVM loginVM = null)
        {
            //Method to create initial accounts and roles
            //await CreateRoleAndAccounts();
            if (User.Identity.IsAuthenticated)
            {
                returnUrl = await ChooseStartPage(returnUrl, User.Identity.Name);
                return Redirect(returnUrl);
            }
            else
                return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<RedirectResult> Logout()
        {
            await _identityService.LogoutAsync();
            return Redirect("LoginPage");
        }
        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LoginPage(LoginVM loginData, string returnUrl, bool registerForm)
        {
            ViewBag.ActiveForm = "LoginForm";
            bool isNullOrEmpty = AdditionalServices.CheckIsNullOrEmpty(loginData);
            if (!isNullOrEmpty)
            {
                if (!registerForm)
                {
                    bool result = await _identityService.LoginAsync(loginData);
                    if (result)
                    {
                        returnUrl = await ChooseStartPage(returnUrl, loginData.Username);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect username or password");
                        return View(loginData);
                    }
                }
                else
                {
                    ViewBag.ActiveForm = "RegisterForm";
                    string message = await _identityService.RegisterAsync(loginData);
                    if (message == "")
                    {
                        returnUrl = await ChooseStartPage(returnUrl, loginData.Username);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", message);
                        return View(loginData);
                    }
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
        private async Task<string> ChooseStartPage(string url, string login)
        {
            bool isAdmin = await _identityService.CheckIsAdminAsync(login);
            if (isAdmin)
            {
                if (String.IsNullOrWhiteSpace(url))
                    url = "/Home/AdministratorPanel";
            }
            else
            {
                if (String.IsNullOrWhiteSpace(url))
                    url = "/Home/UserPanel";
            }

            return url;
        }
        [Authorize(Roles ="Administrator")]
        public IActionResult Accounts(int currentSubView = 1, string errorMessage = null, string feedback = null)
        {
            ViewBag.CurrentSubView = currentSubView;
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.Feedback = feedback;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM passwordVM)
        {
            bool result = false;
            string message = "";
            string action = await ChooseAccountPage(User.Identity.Name);
            if (passwordVM.NewPassword == passwordVM.ConfirmedPassword)
            {
                result = await _identityService.ChangePasswordAsync(passwordVM, User.Identity.Name);
                if (!result)
                {
                    message = "Incorrect password";
                    return RedirectToAction(action, new { errorMessage = message });
                }
                else
                {
                    message = "Password successfully changed";
                    return RedirectToAction(action, new { feedback = message });
                }
            }
            else
                message = "Passwords must be identical";

            return RedirectToAction(action, new { errorMessage = message});
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetApiKey() =>
            Json(await _readingService.GetUserApiKey(User.Identity.Name));

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ChangeApiKey(string NewApiKey)
        {
            string username = User.Identity.Name;
            await _readingService.ChangeUserApiKey(username, NewApiKey);
            string action = await ChooseAccountPage(username);
            return RedirectToAction(action, new { currentSubView = 2});
        }
        private async Task<string> ChooseAccountPage(string login)
        {
            string action = "Account";
            bool isAdmin = await _identityService.CheckIsAdminAsync(login);
            if (isAdmin)
                    action = "Accounts";

            return action;
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUserList([FromBody]string searchPhrase)
        {
            List<FoundUser> users = await _identityService.FindUsersByPhrase(searchPhrase, User.Identity.Name);
            return Json(users);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ChangePermission([FromBody]FoundUser user)
        {
            string message = await _identityService.ChangePermissionAsync(user);

            return Json(message);
        }
        public IActionResult Account(string errorMessage = null, string feedback = null) 
        {
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.Feedback = feedback;

            return View();
        }

    }
}
