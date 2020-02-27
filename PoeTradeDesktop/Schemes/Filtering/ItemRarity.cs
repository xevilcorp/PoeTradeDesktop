using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Filtering
{
    public class ItemRarity
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public static async Task<List<ItemRarity>> LoadAsync()
        {
            return await Task.Run(Load);
        }

        public static List<ItemRarity> Load()
        {
            string path = Path.Combine(Environment.CurrentDirectory, $@"Data\item-rarity.json");
            string json = File.ReadAllText(path, Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<ItemRarity>>(json);
        }
    }
}
