using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WeatherApp.Models;
using Newtonsoft.Json;

namespace WeatherApp.Services
{
    public class WeatherApiService
    {
        private readonly string _apiKey;
        private readonly List<Voivodeship> _voivodeships;
        public string errorMessage;
        //private List<Json>
        public WeatherApiService(string apiKey, List<Voivodeship> voivodeships)
        {
            _apiKey = apiKey;
            _voivodeships = voivodeships;
        }

        public async Task<List<WeatherReadings>> GetCurrentWeatherData()
        {
            List<WeatherReadings> weatherReadings = new List<WeatherReadings>();
            using (WebClient client = new WebClient())
            {
                foreach(var v in _voivodeships)
                {
                    var settings = new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    };
                    Uri uri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q={v.CityName}&appid={_apiKey}");
                    var weatherJson = await client.DownloadStringTaskAsync(uri);
                    WeatherReadings weather = JsonConvert.DeserializeObject<WeatherReadings>(weatherJson, settings);
                    weather.Voivodeship = v;
                    
                    weatherReadings.Add(weather);
                }

            }

            return weatherReadings;
        }

        public async Task<bool> CheckIsApiKeyValid()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    Uri uri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q=Warszawa&appid={_apiKey}");
                    var weatherJson = await client.DownloadStringTaskAsync(uri);
                }
                catch (Exception e)
                {
                    errorMessage = e.Message+" propably your api_key is invalid";
                    return false;
                }
            }
            return true; ;
        }
    }
}
