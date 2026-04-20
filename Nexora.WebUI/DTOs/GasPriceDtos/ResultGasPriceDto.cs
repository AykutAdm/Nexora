namespace Nexora.WebUI.DTOs.GasPriceDtos
{
    public class ResultGasPriceDto
    {

        public bool success { get; set; }
        public Result[] result { get; set; }


        public class Result
        {
            public string currency { get; set; }
            public string lpg { get; set; }
            public string diesel { get; set; }
            public string gasoline { get; set; }
            public string country { get; set; }
        }

    }
}
