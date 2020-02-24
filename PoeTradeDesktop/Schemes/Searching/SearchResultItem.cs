using PoeTradeDesktop.Schemes.Searching;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem;

namespace PoeTradeDesktop.Schemes.Searching
{
    public class SearchResultItem
    {
        public string Id;
        public Listing Listing { get; set; }
        public Item Item { get; set; }

        public SearchResultItem()
        {
            Listing = new Listing();
            Item = new Item();
        }
    }
}
