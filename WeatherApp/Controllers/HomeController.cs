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
    [Authorize]
    public class HomeController: Controller
    {
        private readonly IReadingsService _readingsService;

        public HomeController(IReadingsService readingsService) =>
            _readingsService = readingsService;
        [Authorize]
        public IActionResult HomePage()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult AdministratorPanel()
        {
            return View();
        }
        [Authorize(Roles = "StandardUser, Administrator")]
        public IActionResult UserPanel()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Refresh()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetWeather([FromBody]string voivodeship)
        {
            string voivodeshipName = AdditionalServices.GetVoivodeshipName(voivodeship);
            var model = await _readingsService.GetWeatherAsync(voivodeshipName);
            return Json(model);
        }
    }
}
