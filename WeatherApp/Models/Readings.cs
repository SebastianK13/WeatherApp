using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Readings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public IList<VoivodeshipTemp> Temperatures { get; set; } 
    }

    public class VoivodeshipTemp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Voivodeship { get; set; }
        public double Temperature { get; set; }
        public DateTime ReadingDate { get; set; }
    }

    public class WeatherApiKey
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
}
