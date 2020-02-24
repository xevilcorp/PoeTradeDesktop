using Newtonsoft.Json;
using PoeTradeDesktop.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Searching
{
    public class PreSearchResult
    {
        public string Id;
        public int Total;
        [JsonProperty("result")]
        public List<string> ItemIds;

        public static async Task<PreSearchResult> Load(Search search)
        {
            string url = $"https://www.pathofexile.com/api/trade/search/" + search.League.LeagueId;

            string response = "";
            using (var bench = new Benchmark("Pre search raw response: "))
            {
                response = await Api.PostAsync(url, search);
            }
            PreSearchResult ok = null;
            using (var bench = new Benchmark("Pre search deserialization: "))
            {
                ok = JsonConvert.DeserializeObject<PreSearchResult>(response);
            }
            return ok;
        }
    }
}
