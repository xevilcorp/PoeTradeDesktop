using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item
{
    public class Extended
    {
        public Mods Mods { get; set; }
        public Hashes Hashes { get; set; }

        public float Ar { get; set; }
        public float Ev { get; set; }
        public float Es { get; set; }
        public float Dps { get; set; }
        public float Pdps { get; set; }
        public float Edps { get; set; }

        public bool Ar_aug { get; set; }
        public bool Ev_aug { get; set; }
        public bool Es_aug { get; set; }
        public bool Dps_aug { get; set; }
        public bool Pdps_aug { get; set; }
        public bool Edps_aug { get; set; }
    }
}