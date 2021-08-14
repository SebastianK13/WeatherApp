using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Controllers
{
    public class RefreshController : Controller
    {
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult RefreshData()
        {
            return View();
        }
    }
}
