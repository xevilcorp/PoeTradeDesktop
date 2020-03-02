using System.Collections.Generic;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item
{
    public class Property
    {
        public string Name { get; set; }
        public List<List<string>> Values { get; set; }
        public int DisplayMode { get; set; }
        public int Type { get; set; }
        public double Progress { get; set; }
    }
}
