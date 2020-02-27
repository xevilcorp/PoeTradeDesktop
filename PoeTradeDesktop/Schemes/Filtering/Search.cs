using Newtonsoft.Json;

namespace PoeTradeDesktop.Schemes
{
    public class Search
    {
        [JsonIgnore]
        public League League { get; set; }

        public Query Query { get; set; }
        public Sort Sort { get; set; }
    }
}
