using System.Collections.Generic;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item
{
    public class HybridItem
    {
        public bool IsVaalGem { get; set; }
        public string BaseTypeName { get; set; }
        public string SecDescrText { get; set; }
        public List<Property> Properties { get; set; }
        public List<string> ExplicitMods { get; set; }
    }
}
