using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Context
{
    public class WeatherReadingsDbContext: DbContext
    {
        public WeatherReadingsDbContext(DbContextOptions<WeatherReadingsDbContext> options)
            :base(options) { }

        public DbSet<WeatherReadings> WeatherReadings { get; set; }
        public DbSet<WeatherApiKey> WeatherApiKeys { get; set; }
        public DbSet<Voivodeship> Voivodeships { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<Main> Mains { get; set; }
        public DbSet<Wind> Winds { get; set; }
    }
}
