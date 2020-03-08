using Newtonsoft.Json;
using PoeTradeDesktop.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Filtering
{
    public class SearchItem
    {
        public short Id { get; set; }
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
            short countItem = 1;
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

        public static async Task<List<SearchItem>> GetSearchItemsAsync()
        {
            return await Task.Run(() => GetSearchItems());
        }

        public static List<SearchItem> GetSearchItems(string text)
        {
            List<SearchItem> items = new List<SearchItem>();
            string query = $"SELECT Id, Text, [Unique] FROM SearchItems WHERE Text LIKE '%{text}%'";
            DataTable dt = DB.GetDataTable(query);
            int size = dt.Rows.Count;
            for (int i = 0; i < size; i++)
            {
                items.Add(new SearchItem { 
                    Id = Convert.ToInt16(dt.Rows[i][0]),
                    Text = dt.Rows[i][1].ToString(),
                    //Type = dt.Rows[i][2].ToString(),
                    //Name = dt.Rows[i][3].ToString(),
                    Unique = dt.Rows[i][2] != DBNull.Value ? Convert.ToBoolean(dt.Rows[i][2]) : false
                });
            }
            return items;
        }

        public static async Task<List<SearchItem>> GetAllSearchItemsAsync()
        {
            return await Task.Run(() => GetAllSearchItems());
        }

        public static List<SearchItem> GetAllSearchItems()
        {
            List<SearchItem> items = new List<SearchItem>();
            string query = $"SELECT Id, Text, [Unique] FROM SearchItems";
            DataTable dt = DB.GetDataTable(query);
            int size = dt.Rows.Count;
            for (int i = 0; i < size; i++)
            {
                items.Add(new SearchItem
                {
                    Id = Convert.ToInt16(dt.Rows[i][0]),
                    Text = dt.Rows[i][1].ToString(),
                    //Type = Convert.ToString(dt.Rows[i][2]),
                    //Name =  dt.Rows[i][3] != null ? dt.Rows[i][3].ToString() : null,
                    //Unique = Convert.ToBoolean(dt.Rows[i][4])
                });
            }
            return items;
        }
    }
}
