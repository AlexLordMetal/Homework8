using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChristmasToys
{
    public class WorkWithFiles
    {
        
        public static Products ProductsFromJson(string filenameOrUrl)
        {
            if (filenameOrUrl.StartsWith(@"https://", true, null))
            {
                return JsonConvert.DeserializeObject<Products>(GetStringFromHttp(filenameOrUrl).Result);
            }
            else
            {
                return JsonConvert.DeserializeObject<Products>(GetStringFromFile(filenameOrUrl));
            }
        }

        private static string GetStringFromFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
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