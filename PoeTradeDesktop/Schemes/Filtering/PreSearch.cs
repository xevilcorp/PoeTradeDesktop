using Newtonsoft.Json;

namespace PoeTradeDesktop.Schemes.Filtering
{
    public class PreSearch
    {
        [JsonIgnore]
        public League League { get; set; }

        public dynamic Query { get; set; }
        public Sort Sort { get; set; }
    }
}
