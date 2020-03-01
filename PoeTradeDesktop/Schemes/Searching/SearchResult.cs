using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoeTradeDesktop.Schemes.Filtering;

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

        public async Task Load(PreSearch preSearch)
        {
            Items = new List<SearchResultItem>();

            if (PreSearchResult == null)
                PreSearchResult = await PreSearchResult.Load(preSearch);

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
