using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public interface IReadingsService
    {
        Task<string> GetUserApiKey(string username);
        Task ChangeUserApiKey(string username, string apiKey);
    }
}
