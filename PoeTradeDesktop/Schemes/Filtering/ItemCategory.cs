using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Filtering
{
    public class ItemCategory
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public static async Task<List<ItemCategory>> LoadAsync()
        {
            return await Task.Run(Load);
        }

        public static List<ItemCategory> Load()
        {
            string path = Path.Combine(Environment.CurrentDirectory, $@"Data\item-category.json");
            string json = File.ReadAllText(path, Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<ItemCategory>>(json);
        }
    }
}

