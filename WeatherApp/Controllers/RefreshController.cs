using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RefreshController : Controller
    {
        private readonly IReadingsService _readingsService;

        public RefreshController(IReadingsService readingsService)
        {
            _readingsService = readingsService;
        }
        [HttpPost]
        public async Task<IActionResult> RefreshData()
        {
            List<WeatherReadings> readings = new List<WeatherReadings>();
            string api_key = await _readingsService.GetUserApiKey(User.Identity.Name);

            if (api_key != null)
            {
                List<Voivodeship> voivodeships = await _readingsService.GetVoivodeshipsAsync();
                WeatherApiService weatherApi = new WeatherApiService(api_key, voivodeships);
                bool result = await weatherApi.CheckIsApiKeyValid();
                if (result)
                {
                    readings = await weatherApi.GetCurrentWeatherData();
                    await _readingsService.UpdateWeatherReadingsAsync(readings);
                }
                else
                    return RedirectToAction("RefreshPage", new { errorMessage = weatherApi.errorMessage });
            }

            return RedirectToAction("RefreshPage", new { feedback = "Data successfully refreshed" });
        }
        public IActionResult RefreshPage(string errorMessage = null, string feedback = null)
        {
            //Voivodeships inserting to database
            //await _readingsService.InsertVoivodeshipData(voivodeships);

            if (errorMessage != null)
                ViewBag.ApiError = errorMessage;
            else if (feedback != null)
                ViewBag.Feedback = feedback;

            return View();
        }
    }
}
