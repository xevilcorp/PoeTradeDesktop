using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended._Mods;
using System.Collections.Generic;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended
{
    public class Mods
    {
        public List<Implicit> Implicit { get; set; }
        public List<Explicit> Explicit { get; set; }
        public List<Crafted> Crafted { get; set; }

    }
}