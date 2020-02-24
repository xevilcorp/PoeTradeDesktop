using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoeTradeDesktop.Schemes.Searching
{
    public class SearchResult
    {
        public int PageSize = 10;
        public int TotalLoaded = 0;
        public int TotalAvailableToLoad = 1;
        public PreSearchResult PreSearchResult;
        public List<SearchResultItem> Items = new List<SearchResultItem>();

        public async void Load(Search search)
        {
            if(PreSearchResult == null)
            PreSearchResult = await PreSearchResult.Load(search);

            int size = PageSize;
            TotalAvailableToLoad = PreSearchResult.Total - TotalLoaded;
            if (TotalAvailableToLoad < PageSize) size = TotalAvailableToLoad;

            if(size != 0) 
            {
                List<string> idsToLoad = PreSearchResult.ItemIds.Take(size).ToList();
                PreSearchResult.ItemIds.RemoveRange(0, size);

                string url = "https://www.pathofexile.com/api/trade/fetch/" + string.Join(",", idsToLoad) + "?query=" + PreSearchResult.Id;

                string response = await Api.GetAsync(url);

                dynamic o = JsonConvert.DeserializeObject(response);

                foreach(var r in o.result)
                {
                    SearchResultItem i = new SearchResultItem();

                    i.Id = r.id;

                    i.Account.Name = r.account.name;
                    i.Account.Language = r.account.language;
                    i.Account.Whisper = r.account.whisper;
                    i.Account.LastCharacterName = r.account.lastCharacterName;

                    i.Price.Amount = r.info.price.ammount;
                    i.Price.Currency = r.info.price.currency;
                    i.Price.Type = r.info.price.type;

                    i.Item.Icon = r.item.icon;
                    i.Item.Identified = r.item.identified;
                    i.Item.Name = r.item.name;
                    i.Item.ILvl = r.item.ilvl;
                    i.Item.Verified = r.item.verified;
                    i.Item.Corrupted = r.item.corruped;

                    Items.Add(i);
                }
            }
        }
    }
}
