namespace Nexora.WebUI.DTOs.ExchangeRateDtos
{
    public class ResultExchangeRateDto
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string _base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }

        public class Rates
        {
            public float EUR { get; set; }
            public float USD { get; set; }
            public float RUB { get; set; }
        }

    }
}
