using Newtonsoft.Json;

namespace ChristmasToys
{
    public class Price
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}