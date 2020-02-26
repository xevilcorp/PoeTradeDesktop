using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item
{
    public class Extended
    {
        public Mods Mods { get; set; }
        public Hashes Hashes { get; set; }

        public int Ar { get; set; }
        public int Ev { get; set; }
        public int Es { get; set; }
        public int Dps { get; set; }
        public int Pdps { get; set; }
        public int Edps { get; set; }

        public bool Ar_aug { get; set; }
        public bool Ev_aug { get; set; }
        public bool Es_aug { get; set; }
        public bool Dps_aug { get; set; }
        public bool Pdps_aug { get; set; }
        public bool Edps_aug { get; set; }
    }
}