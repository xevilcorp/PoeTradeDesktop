using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PoeTradeDesktop.Util;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoeTradeDesktop
{
    class Api
    {
        public static readonly HttpClient Client = new HttpClient();

        public static async Task<string> PostAsync(string url, object o)
        {
            return await Task.Run(() => Post(url, o));
        }

        public static string Post(string url, object o)
        {
            JsonSerializerSettings serializer = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string json = JsonConvert.SerializeObject(o, Formatting.None, serializer);

            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            request.ContentType = "application/json";
            request.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            string result = "";
            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        public static async Task<string> GetAsync(string url)
        {
            var response = await Client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<Stream> GetStreamAsync(string url)
        {
            var response = await Client.GetAsync(url);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
