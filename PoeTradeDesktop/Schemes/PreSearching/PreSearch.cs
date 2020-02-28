using Newtonsoft.Json;
using PoeTradeDesktop.Schemes.PreSearching._PreSearch;

namespace PoeTradeDesktop.Schemes.PreSearching
{
    public class PreSearch
    {
        [JsonIgnore]
        public League League { get; set; }

        public dynamic Query { get; set; }
        public Sort Sort { get; set; }
    }
}
