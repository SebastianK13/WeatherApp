using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IReadingsService
    {
        Task<string> GetUserApiKey(string username);
        Task ChangeUserApiKey(string username, string apiKey);
        Task InsertVoivodeshipData(List<Voivodeship> voivodeships);
        Task<List<Voivodeship>> GetVoivodeshipsAsync();
        Task UpdateWeatherReadingsAsync(List<WeatherReadings> readings);
        Task<List<WeatherReadings>> GetWeatherAsync(string voivodeship);
    }
}
