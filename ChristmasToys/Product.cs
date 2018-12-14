using Newtonsoft.Json;
using System;

namespace ChristmasToys
{
    public class Product
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("name_prefix")]
        public string PrefixName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("html_url")]
        public string URL { get; set; }

        [JsonProperty("prices")]
        public Prices Prices { get; set; }

        public void ToConsole()
        {
            Console.WriteLine($"{PrefixName} {FullName} (Цена: {Prices.PriceMin.Amount} BYN)");
        }

    }
}