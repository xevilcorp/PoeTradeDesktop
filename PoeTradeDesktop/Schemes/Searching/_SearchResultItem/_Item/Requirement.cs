using System.Collections.Generic;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item
{
    public class Requirement
    {
        public string Name { get; set; }
        public List<List<string>> Values { get; set; }
        public int DisplayMode { get; set; }
    }
}