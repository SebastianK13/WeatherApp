using Microsoft.AspNetCore.Identity;
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

        public async Task SetStandardUserAccountAsync(LoginVM loginData)
        {
            IdentityUser user = await CreateAccountAsync(loginData);
            if (user != null)
            {
                IdentityRole role = await _roleManager.FindByNameAsync("StandardUser");
                if(role != null)
                    await _userManager.AddToRoleAsync(user, role.Name);
            }
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
    }
}
