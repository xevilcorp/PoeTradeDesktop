using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System.Collections.Generic;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem
{
    public class Item
    {
        public bool Verified { get; set; }
        public int ILvl { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string TypeLine { get; set; }
        public bool Corrupted { get; set; }
        public bool Identified { get; set; }
        public Influences Influences { get; set; }
        public int FrameType { get; set; }
        public int MaxStackSize { get; set; }
        public int StackSize { get; set; }
        public List<Property> Properties { get; set; }
        public List<Socket> Sockets { get; set; }
        public List<Requirement> Requirements { get; set; }
        public List<string> ExplicitMods { get; set; }
        public List<string> ImplicitMods { get; set; }
        public List<string> EnchantMods { get; set; }
        public List<string> CraftedMods { get; set; }
        public Extended Extended { get; set; }
    }
}
