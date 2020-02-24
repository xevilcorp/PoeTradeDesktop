using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Listing;
using System;

namespace PoeTradeDesktop.Schemes.Searching._SearchResultItem
{
    public class Listing
    {
        public Account Account { get; set; }
        public Price Price { get; set; }
        public string Whisper { get; set; }
        public DateTime Indexed { get; set; }

        public Listing()
        {
            Account = new Account();
            Price = new Price();
        }
    }
}
