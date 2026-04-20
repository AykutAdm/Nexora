namespace Nexora.WebUI.DTOs.CryptoPriceDtos
{
    public class ResultCryptoPriceDto
    {
        public Bitcoin bitcoin { get; set; }
        public Ethereum ethereum { get; set; }

        public class Bitcoin
        {
            public int usd { get; set; }
        }

        public class Ethereum
        {
            public float usd { get; set; }
        }
    }
}
