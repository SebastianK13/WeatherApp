using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models.ViewModels;

namespace WeatherApp.Services
{
    public static class AdditionalServices
    {
        public static bool CheckIsNullOrEmpty(LoginVM loginData)
        {
            if (String.IsNullOrWhiteSpace(loginData.Username) ||
                String.IsNullOrWhiteSpace(loginData.Password))
            {
                return true;
            }
            else
                return false;
        }
    }
}
