using Newtonsoft.Json;
using System.IO;

namespace ChristmasToys
{
    abstract class WorkWithFiles
    {

        public static Products ProductsFromJson(string fileName)
        {
            using (var reader = new StreamReader("PresentsJson.txt"))
            {
                return JsonConvert.DeserializeObject<Products>(reader.ReadToEnd());
            }
        }

    }
}