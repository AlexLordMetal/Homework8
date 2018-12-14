using Newtonsoft.Json;

namespace ChristmasToys
{
    public class Prices
    {
        [JsonProperty("price_min")]
        public Price PriceMin { get; set; }
    }
}