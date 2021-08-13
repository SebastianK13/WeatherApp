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
        Task SetStandardUserAccountAsync(LoginVM loginData);
        Task CreateRolesAsync(List<string> roles);
        Task<bool> LoginAsync(LoginVM loginData);
        Task<bool> CheckIsAdminAsync(string login);
    }
}
