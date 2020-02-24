using Newtonsoft.Json;

namespace PoeTradeDesktop.Schemes
{
    public class Search
    {
        [JsonIgnore]
        public League League = new League();

        public Query Query = new Query();
        public Sort Sort = new Sort();
    }
}
