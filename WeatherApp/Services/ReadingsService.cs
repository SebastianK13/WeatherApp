using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Context;

namespace WeatherApp.Services
{
    public class ReadingsService : IReadingsService
    {
        private readonly WeatherReadingsDbContext _context;

        public ReadingsService(WeatherReadingsDbContext context) =>
            _context = context;

        public async Task ChangeUserApiKey(string username, string apiKey)
        {
            var userApiKey = await _context.WeatherApiKeys
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            userApiKey.ApiKey = apiKey;
            _context.Update(userApiKey);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetUserApiKey(string username) =>
            await _context.WeatherApiKeys.Where(u => u.Username == username)
                .Select(k => k.ApiKey)
                .FirstOrDefaultAsync();

    }
}
