using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models.ViewModels;

namespace WeatherApp.Services
{
    public class IdentityService: IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task LogoutAsync() =>
            await _signInManager.SignOutAsync();

        public async Task SetAdminAccountAsync(LoginVM loginData)
        {
            IdentityUser user = await CreateAccountAsync(loginData);
            if (user != null)
            {
                IdentityRole role = await _roleManager.FindByNameAsync("Administrator");
                if (role != null)
                    await _userManager.AddToRoleAsync(user, role.Name);
            }
        }

        public async Task<bool> SetStandardUserAccountAsync(LoginVM loginData)
        {
            IdentityUser user = await CreateAccountAsync(loginData);
            if (user != null)
            {
                IdentityRole role = await _roleManager.FindByNameAsync("StandardUser");
                if(role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                    return true;
                }
            }
            return false;
        }

        private async Task<IdentityUser> CreateAccountAsync(LoginVM loginData)
        {
            IdentityUser user = await _userManager.FindByNameAsync(loginData.Username);

            if (user == null)
            {
                user = new IdentityUser(loginData.Username);
                if ((await _userManager.CreateAsync(user, loginData.Password)).Succeeded)
                    return user;
            }

            return null;
        }
        public async Task CreateRolesAsync(List<string> roles) 
        {
            foreach(var r in roles)
            {
                bool exist = await _roleManager.RoleExistsAsync(r);
                if (!exist)
                {
                    IdentityRole role = new IdentityRole(r);
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        public async Task<bool> LoginAsync(LoginVM loginData)
        {
            IdentityUser user = await _userManager.FindByNameAsync(loginData.Username);
            if (user != null)
            {
                bool result = (await _signInManager.PasswordSignInAsync(user, loginData.Password, false, false)).Succeeded;
                if (result)
                    return true;
            }

            return false;
        }

        public async Task<bool> CheckIsAdminAsync(string login)
        {
            IdentityUser user = await _userManager.FindByNameAsync(login);
            return await _userManager.IsInRoleAsync(user, "Administrator");
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordVM passwordVM, string username)
        {
            IdentityUser user = await _userManager.FindByNameAsync(username);
            bool result = (await _userManager.ChangePasswordAsync(user, passwordVM.CurrentPassword, passwordVM.NewPassword)).Succeeded;

            return result;
        }

        public async Task<List<FoundUser>> FindUsersByPhrase(string searchPhrase, string username)
        {
            List<FoundUser> foundUsers = new List<FoundUser>();
            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(searchPhrase) && u.UserName != username)
                .Take(5)
                .Select(x => x.UserName)
                .ToListAsync();

            foreach(var u in users)
            {
                IdentityUser user = await _userManager.FindByNameAsync(u);
                FoundUser foundUser = new FoundUser();
                foundUser.Username = u;
                foundUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                foundUsers.Add(foundUser);
                foundUser.ToRole = await _roleManager.Roles
                    .Where(r => r.Name == "Administrator")
                    .Select(n => n.Name)
                    .FirstOrDefaultAsync();
            }

            return foundUsers;
        }

        public async Task<string> ChangePermissionAsync(FoundUser user)
        {
            string message = "";
            IdentityUser identity = await _userManager.FindByNameAsync(user.Username);
            var r = await _userManager.AddToRoleAsync(identity, user.ToRole);
            await _userManager.RemoveFromRoleAsync(identity, user.Role);

            if (!r.Succeeded)
                message = r.Errors.ToString();
            else
                message = "Role changing complete succefully";

            return message;
        }

        public async Task<string> RegisterAsync(LoginVM loginData)
        {
            string message = "";
            IdentityUser user = await _userManager.FindByNameAsync(loginData.Username);
            if (user == null)
            {
                bool result = await SetStandardUserAccountAsync(loginData);
                if (!result)
                    message = "Password does not fit to security requirements";
            }
            else
                message = "This username is allready taken";

            return message;
        }
    }
}
