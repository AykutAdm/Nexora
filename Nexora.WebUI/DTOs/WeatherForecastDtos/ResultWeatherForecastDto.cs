using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexora.WebUI.DTOs.WeatherForecastDtos
{
    public class ResultWeatherForecastDto
    {
        public string name { get; set; }
        public MainWeatherBlock main { get; set; }
        public List<WeatherCondition> weather { get; set; }
        public WindBlock wind { get; set; }
        public SysBlock sys { get; set; }
    }

    public class MainWeatherBlock
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public int humidity { get; set; }
    }

    public class WeatherCondition
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class WindBlock
    {
        public float speed { get; set; }
        public int? deg { get; set; }
    }

    public class SysBlock
    {
        public string country { get; set; }
    }
}
