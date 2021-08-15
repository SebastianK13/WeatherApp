using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Context;
using WeatherApp.Models;

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

        public async Task InsertVoivodeshipData(List<Voivodeship> voivodeships)
        {
            await _context.Voivodeships.AddRangeAsync(voivodeships);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Voivodeship>> GetVoivodeshipsAsync() =>
            await _context.Voivodeships.ToListAsync();

        public async Task UpdateWeatherReadingsAsync(List<WeatherReadings> readings)
        {
            await _context.WeatherReadings.AddRangeAsync(readings);
            await _context.SaveChangesAsync();
        }
    }
}
