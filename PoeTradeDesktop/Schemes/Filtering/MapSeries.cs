using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Filtering
{
    public class MapSeries
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public static async Task<List<MapSeries>> LoadAsync()
        {
            return await Task.Run(Load);
        }

        public static List<MapSeries> Load()
        {
            string path = Path.Combine(Environment.CurrentDirectory, $@"Data\map-series.json");
            string json = File.ReadAllText(path, Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<MapSeries>>(json);
        }
    }
}
