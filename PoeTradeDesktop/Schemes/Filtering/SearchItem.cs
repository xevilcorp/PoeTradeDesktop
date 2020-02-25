using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Models
{
    public class SearchItem
    {
        public int Id { get; set; }
       // public ItemCategory ItemCategory { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        //public string Disc { get; set; }
        public string Text { get; set; }
        public bool Unique { get; set; }

        public static async Task<List<SearchItem>> GetSearchItems()
        {
            string response = await Api.GetAsync("https://www.pathofexile.com/api/trade/data/items");
            dynamic o = JsonConvert.DeserializeObject(response);
            List<SearchItem> searchItems = new List<SearchItem>();
            //int countCategory = 1;
            int countItem = 1;
            foreach (var category in o.result)
            {
                //ItemCategory ic = new ItemCategory { Id = countCategory, Text = category.label };
                foreach (var item in category.entries)
                {
                    searchItems.Add(new SearchItem
                    {
                        Id = countItem,
                        //ItemCategory = ic,
                        Text = item.text,
                        Name = item.name != null ? item.name : null,
                        Type = item.type,
                        //Disc = item.disc != null ? item.disc : null,
                        Unique = item.flags != null
                    });
                    countItem++;
                }
                //countCategory++;
            }
            return searchItems;
        }
    }
}
