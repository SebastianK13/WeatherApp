using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {

        public IActionResult About()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult AboutAdmin()
        {
            return View();
        }
    }
}
