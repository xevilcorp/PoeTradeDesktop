using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended._Mods;
using System.Collections.Generic;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended
{
    public class Mods
    {
        public List<ModDetails> Implicit { get; set; }
        public List<ModDetails> Explicit { get; set; }
        public List<ModDetails> Crafted { get; set; }
        public List<ModDetails> Enchant { get; set; }

    }
}