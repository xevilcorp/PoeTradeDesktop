using PoeTradeDesktop.Models;
using PoeTradeDesktop.Schemes;
using PoeTradeDesktop.Schemes.PreSearching;
using PoeTradeDesktop.Schemes.PreSearching._PreSearch;
using PoeTradeDesktop.Schemes.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers
{
    class SearchControl : BaseControl
    {
        #region Properties
        private bool itemTypeFilterEnabled;
        public bool ItemTypeFilterEnabled
        {
            get { return itemTypeFilterEnabled; }
            set { itemTypeFilterEnabled = value; RaisePropertyChanged("ItemTypeFilterEnabled"); }
        }

        private object itemTypeFilterTabContent;
        public object ItemTypeFilterTabContent
        {
            get { return itemTypeFilterTabContent; }
            set { itemTypeFilterTabContent = value; RaisePropertyChanged("ItemTypeFilterTabContent"); }
        }

        private List<League> leagues;
        public List<League> Leagues
        {
            get { return leagues; }
            set { leagues = value; RaisePropertyChanged("Leagues"); }
        }

        private bool isFilterContentHidden;
        public bool IsFilterContentHidden
        {
            get { return isFilterContentHidden; }
            set { isFilterContentHidden = value; RaisePropertyChanged("IsFilterContentHidden"); }
        }

        private List<SearchItem> searchTextItems;
        public List<SearchItem> SearchTextItems
        {
            get { return searchTextItems; }
            set { searchTextItems = value; RaisePropertyChanged("SearchTextItems"); }
        }

        private List<SearchItem> searchTextItemsResult;
        public List<SearchItem> SearchTextItemsResult
        {
            get { return searchTextItemsResult; }
            set { searchTextItemsResult = value; RaisePropertyChanged("SearchTextItemsResult"); }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisePropertyChanged("SearchText");
                PerformSearch();
            }
        }

        private SearchItem selectedSearchTextResult;
        public SearchItem SelectedSearchTextResult
        {
            get { return selectedSearchTextResult; }
            set { 
                selectedSearchTextResult = value; 
                if(selectedSearchTextResult.Name.IndexOf("Geofri") != -1)
                {
                    int x = 1;
                }
                RaisePropertyChanged("SelectedSearchTextResult"); }
        }

        private bool isSearchTextListOpened;
        public bool IsSearchTextListOpened
        {
            get { return isSearchTextListOpened; }
            set { isSearchTextListOpened = value; RaisePropertyChanged("IsSearchTextListOpened"); }
        }

        private League selectedLeague;
        public League SelectedLeague
        {
            get { return selectedLeague; }
            set { selectedLeague = value; RaisePropertyChanged("SelectedLeague"); }
        }

        private int selectedOnlineOption;
        public int SelectedOnlineOption
        {
            get { return selectedOnlineOption; }
            set { selectedOnlineOption = value; RaisePropertyChanged("SelectedOnlineOption"); }
        }

        private bool isSearchResultLoading;
        public bool IsSearchResultLoading
        {
            get { return isSearchResultLoading; }
            set { isSearchResultLoading = value; RaisePropertyChanged("IsSearchResultLoading"); }
        }

        private SearchResult searchResult;
        public SearchResult SearchResult
        {
            get { return searchResult; }
            set { searchResult = value; RaisePropertyChanged("SearchResult"); }
        }

        private PreSearch preSearch;
        public PreSearch PreSearch
        {
            get { return preSearch; }
            set { preSearch = value; RaisePropertyChanged("PreSearch"); }
        }

        private Visibility searchResultLoadingVisibility;
        public Visibility SearchResultLoadingVisibility
        {
            get { return searchResultLoadingVisibility; }
            set { searchResultLoadingVisibility = value; RaisePropertyChanged("SearchResultLoadingVisibility"); }
        }

        #endregion Properties

        public SearchControl()
        {
            LoadLeaguesAsync();
            LoadSearchItemsAsync();

            SearchResultLoadingVisibility = Visibility.Collapsed;
            CheckSelectionCMD = new RelayCommand(CheckSelection);
            SearchCMD = new RelayCommand(Search);
            SearchResult = new SearchResult();
        }

        #region Commands
        public ICommand CheckSelectionCMD { get; set; }
        private void CheckSelection(object o)
        {
            switch (Convert.ToInt32(o))
            {
                case 1:
                    if (ItemTypeFilterTabContent == null) ItemTypeFilterTabContent = new Interface.Filters.ItemType(this);
                    ((ItemTypeFilterTabContent as Interface.Filters.ItemType).DataContext as Filters.ItemTypeFilterControl).FilterEnabled = true;
                    break;
            }
        }

        private dynamic AddTabFilters()
        {
            return new
            {
                type_filters = ItemTypeFilterEnabled ? ((Filters.ItemTypeFilterControl)((Interface.Filters.ItemType)ItemTypeFilterTabContent).DataContext).GetFilter() : null
            };
        }

        public ICommand SearchCMD { get; set; }
        private async void Search(object o)
        {
            IsSearchResultLoading = true;
            SearchResult = null;

            PreSearch = new PreSearch();

            PreSearch.League = SelectedLeague;

            dynamic q = new
            {
                type = SelectedSearchTextResult != null ? SelectedSearchTextResult.Type : null,
                name = SelectedSearchTextResult != null ? SelectedSearchTextResult.Name : null,
                status = new {
                    option = SelectedOnlineOption == 0 ? "online" : "any"
                },
                filters = AddTabFilters()
            };

            

            PreSearch.Query = q;

            Sort sort = new Sort();
            sort.Price = "desc";
            PreSearch.Sort = sort;

            SearchResult sr = new SearchResult();
            await sr.Load(PreSearch);
            SearchResult = sr;
            IsSearchResultLoading = false;
        }

        public async void SearchLoadMore()
        {
            await SearchResult.LoadMore();
            IsSearchResultLoading = false;
        }


        #endregion Commands

        #region Functions
        private async void LoadLeaguesAsync()
        {
            Leagues = await League.GetLeagues();
        }

        private async void LoadSearchItemsAsync()
        {
            SearchTextItems = await SearchItem.GetSearchItems();
        }

        private async void PerformSearchAsync()
        {
            await Task.Run(PerformSearch);
        }

        private void PerformSearch()
        {
           
            if (SearchText.Trim() != "")
            {
                List<SearchItem> si = SearchTextItems.Where(x => x.Text.ToUpper().IndexOf(SearchText.ToUpper()) != -1).ToList();
                if (si.Count != 0)
                {
                    if (SelectedSearchTextResult != null)
                    {
                        if (SelectedSearchTextResult.Name != null)
                        {
                            SelectedSearchTextResult = si.First();
                        }
                    }
                    else
                    {
                        SelectedSearchTextResult = si.First();
                    }
                }
                SearchTextItemsResult = si;
            }
            else
            {
                SelectedSearchTextResult = null;
            }
        }
        #endregion Functions
    }
}
