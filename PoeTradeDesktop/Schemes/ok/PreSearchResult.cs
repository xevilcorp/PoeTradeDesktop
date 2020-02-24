using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Searching
{
    public class PreSearchResult
    {
        public string Id;
        public int Total;
        public List<string> ItemIds;

        public static async Task<PreSearchResult> Load(Search search)
        {
            string url = $"https://www.pathofexile.com/api/trade/search/" + search.League.Id;

            string response = await Api.PostAsync(url, search);

            dynamic o = JsonConvert.DeserializeObject(response);

            return new PreSearchResult { Id = o.id, Total = o.total, ItemIds = o.result };
        }
    }
}
