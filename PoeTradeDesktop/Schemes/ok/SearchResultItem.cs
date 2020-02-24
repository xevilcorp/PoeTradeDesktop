using PoeTradeDesktop.Schemes.Searching.ResultItem;

namespace PoeTradeDesktop.Schemes.Searching
{
    public class SearchResultItem
    {
        public string Id;
        public Account Account = new Account();
        public Price Price = new Price();
        public Item Item = new Item();
    }
}
