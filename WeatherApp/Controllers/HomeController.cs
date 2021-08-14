using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Controllers
{
    [Authorize]
    public class HomeController: Controller
    {
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
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task RefreshData()
        {
            
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Refresh()
        {
            return View();
        }
    }
}
