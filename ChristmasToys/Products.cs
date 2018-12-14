using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChristmasToys
{
    public class Products
    {
        [JsonProperty("products")]
        public List<Product> ProductsList { get; set; }

        public void ToConsole()
        {
            for (var index = 0; index < ProductsList.Count; index++)
            {
                Console.Write($"\t{index + 1}. ");
                ProductsList[index].ToConsole();
            }
        }

    }
}
