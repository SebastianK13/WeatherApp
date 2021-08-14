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

        public DbSet<Readings> WeatherReadings { get; set; }
        public DbSet<VoivodeshipTemp> VoivodeshipTemps { get; set; }
        public DbSet<WeatherApiKey> WeatherApiKeys { get; set; }
    }
}
