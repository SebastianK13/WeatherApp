using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Context;
using System.IO;
using WeatherApp.Services;
using Microsoft.AspNetCore.Identity;

namespace WeatherApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            string path = Directory.GetCurrentDirectory();
            services.AddDbContext<WeatherAppIdentityDbContext>(options => 
                options.UseSqlServer(Configuration["Data:WAIdentity:ConnectionString"].Replace("[DataDirectory]", path)));

            services.AddDbContext<WeatherReadingsDbContext>(options =>
                options.UseSqlServer(Configuration["Data:WAReadings:ConnectionString"].Replace("[DataDirectory]", path)));

            services.AddRazorPages();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                options.User.RequireUniqueEmail = false)
            .AddEntityFrameworkStores<WeatherAppIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IReadingsService, ReadingsService>();

            services.ConfigureApplicationCookie(options => 
            {
                options.LoginPath = $"/Account/LoginPage";
                options.LogoutPath = $"/Account/Logout";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LoginPage}");
            });
        }
    }
}
