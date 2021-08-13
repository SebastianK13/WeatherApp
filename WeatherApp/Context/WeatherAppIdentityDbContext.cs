using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Context
{
    public class WeatherAppIdentityDbContext:IdentityDbContext<IdentityUser>
    {
        public WeatherAppIdentityDbContext(DbContextOptions<WeatherAppIdentityDbContext> options) 
            : base(options) { }


    }
}
