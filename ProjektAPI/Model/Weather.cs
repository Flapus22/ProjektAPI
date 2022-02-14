using System;
using System.Collections.Generic;

namespace ProjektAPI.Model
{
    public partial class Weather
    {
        public int IdWeather { get; set; }
        public DateTime Date { get; set; }
        public float Temperature { get; set; }
        public float Presure { get; set; }
        public float? CloudCover { get; set; }
        public float? RainFall { get; set; }
    }
}
