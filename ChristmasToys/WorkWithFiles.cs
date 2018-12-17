using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChristmasToys
{
    public class WorkWithFiles
    {

        public static Products ProductsFromJson(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return JsonConvert.DeserializeObject<Products>(reader.ReadToEnd());
            }
        }

        public static Products ProductsFromHttpJson(string url)
        {
            return JsonConvert.DeserializeObject<Products>(GetStringFromHttp(url).Result);
        }

        private static async Task<string> GetStringFromHttp(string url)
        {
            using (var httpReader = new HttpClient())
            {
                return await httpReader.GetStringAsync(url);
            } 
        }

    }
}