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

        public async Task<List<WeatherReadings>> GetWeatherAsync(string voivodeship)
        {
            List<WeatherReadings> weathers = new List<WeatherReadings>();
            if (voivodeship == "lubuskie" || voivodeship == "kujawsko-pomorskie")
                weathers = await GetWeatherListAsync(voivodeship);
            else
            {
                weathers.Add(await _context.WeatherReadings
                    .Where(v => v.Voivodeship.VoivodeshipName == voivodeship)
                    .OrderByDescending(d => d.Date)
                    .Take(1).Include(w => w.Weathers).Include(m => m.Main).Include(w => w.Wind).Include(v => v.Voivodeship)
                    .FirstOrDefaultAsync());
            }

            return weathers;
        }
        //get weather list for voivodeship where is more than one voivodeship's city
        private async Task<List<WeatherReadings>> GetWeatherListAsync(string voivodeship)
        {
            List<WeatherReadings> weathers = new List<WeatherReadings>();
            weathers = await _context.WeatherReadings
                .Where(v => v.Voivodeship.VoivodeshipName == voivodeship)
                .OrderByDescending(d => d.Date)
                .Take(2).Include(w => w.Weathers).Include(m => m.Main).Include(w => w.Wind).Include(v => v.Voivodeship)
                .ToListAsync();

            return weathers;
        }
    }
}
