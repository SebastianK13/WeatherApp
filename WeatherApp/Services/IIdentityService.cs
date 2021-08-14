using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models.ViewModels;

namespace WeatherApp.Services
{
    public interface IIdentityService
    {
        Task LogoutAsync();
        Task SetAdminAccountAsync(LoginVM loginData);
        Task<bool> SetStandardUserAccountAsync(LoginVM loginData);
        Task CreateRolesAsync(List<string> roles);
        Task<bool> LoginAsync(LoginVM loginData);
        Task<bool> CheckIsAdminAsync(string login);
        Task<bool> ChangePasswordAsync(ChangePasswordVM passwordVM, string username);
        Task<List<FoundUser>> FindUsersByPhrase(string searchPhrase, string username);
        Task<string> ChangePermissionAsync(FoundUser user);
        Task<string> RegisterAsync(LoginVM loginData);
    }
}
