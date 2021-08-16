using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherReadings
    {
        public WeatherReadings() { }
        [JsonConstructor]
        public WeatherReadings(List<Weather> weather, Main main, Wind wind, int dt)
        {
            Weathers = weather;
            Main = new Main(main);
            Wind = wind;
            Date = ConvertSecondsToLocalTime(dt);
        }
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public List<Weather> Weathers { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Voivodeship Voivodeship { get; set; }
        public DateTime Date { get; set; }
        public DateTime ConvertSecondsToLocalTime(int dt) =>
            (new DateTime(1970, 1, 1)).AddSeconds(dt).ToLocalTime();
    }
    public class Voivodeship
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string CityName { get; set; }
        public string VoivodeshipName { get; set; }
    }

    public class WeatherApiKey
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
    public class Weather
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }

    }
    public class Main
    {
        const double kelvin = 273.15;
        public Main() { }
        public Main(Main main)
        {
            Temp = Math.Round(main.Temp - kelvin, 1);
            Feels_like = Math.Round(main.Feels_like - kelvin, 1);
            Temp_min = Math.Round(main.Temp_min - kelvin, 1);
            Temp_max = Math.Round(main.Temp_max - kelvin, 1);
            Pressure = main.Pressure;
            Humidity = main.Humidity;
        }
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }
    public class Wind
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public double Speed { get; set; }
        public int Deg { get; set; }
    }
}
