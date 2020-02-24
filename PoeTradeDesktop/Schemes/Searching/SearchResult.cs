using Newtonsoft.Json;
using PoeTradeDesktop.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Searching
{
    public class SearchResult
    {
        public int PageSize = 9;
        public int TotalLoaded = 0;
        public int TotalAvailableToLoad = 1;
        public PreSearchResult PreSearchResult;
        [JsonProperty("result")]
        public List<SearchResultItem> Items { get; set; }

        public async Task Load(Search search)
        {
            Items = new List<SearchResultItem>();

            if (PreSearchResult == null)
                PreSearchResult = await PreSearchResult.Load(search);

            await LoadMore();
        }

        public async Task LoadMore()
        {
            int size = PageSize;
            TotalAvailableToLoad = PreSearchResult.Total - TotalLoaded;
            if (TotalAvailableToLoad < PageSize) size = TotalAvailableToLoad;

            if (size != 0)
            {
                List<string> idsToLoad = PreSearchResult.ItemIds.Take(size).ToList();
                PreSearchResult.ItemIds.RemoveRange(0, size);

                string url = "https://www.pathofexile.com/api/trade/fetch/" + string.Join(",", idsToLoad) + "?query=" + PreSearchResult.Id;

                string response = await Api.GetAsync(url);

                Items = JsonConvert.DeserializeObject<SearchResult>(response).Items;

                TotalLoaded += size;
                TotalAvailableToLoad = PreSearchResult.Total - TotalLoaded;

            }
        }
    }
}
