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

        public static string GetVoivodeshipName(string voivodeshipViewId)
        {
            string name = voivodeshipViewId;
            switch (voivodeshipViewId)
            {
                case "wmazurskie":
                    name = "warmińsko-mazurskie";
                    break;
                case "malopolskie":
                    name = "małopolskie";
                    break;
                case "slaskie":
                    name = "śląskie";
                    break;
                case "dslaskie":
                    name = "dolnośląskie";
                    break;
                case "zpomorskie":
                    name = "zachodniopomorskie";
                    break;
                case "kpomorskie":
                    name = "kujawsko-pomorskie";
                    break;
                case "swietokrzyskie":
                    name = "świętokrzyskie";
                    break;
                case "lodzkie":
                    name = "łódzkie";
                    break;
                default:
                    break;
            }
            return name;
        }
    }
}
