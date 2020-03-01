using PoeTradeDesktop.Schemes;
using PoeTradeDesktop.Schemes.Filtering;
using PoeTradeDesktop.Schemes.Searching;
using PoeTradeDesktop.Controllers.FilterTabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PoeTradeDesktop.UI.Components.FilterTabs;

namespace PoeTradeDesktop.Controllers
{
    class SearchControl : BaseControl
    {
        #region Properties

        #region filterTabs 
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

        private bool weaponFilterEnabled;
        public bool WeaponFilterEnabled
        {
            get { return weaponFilterEnabled; }
            set { weaponFilterEnabled = value; RaisePropertyChanged("WeaponFilterEnabled"); }
        }

        private object weaponFilterTabContent;
        public object WeaponFilterTabContent
        {
            get { return weaponFilterTabContent; }
            set { weaponFilterTabContent = value; RaisePropertyChanged("WeaponFilterTabContent"); }
        }

        private bool armourFilterEnabled;
        public bool ArmourFilterEnabled
        {
            get { return armourFilterEnabled; }
            set { armourFilterEnabled = value; RaisePropertyChanged("ArmourFilterEnabled"); }
        }

        private object armourFilterTabContent;
        public object ArmourFilterTabContent
        {
            get { return armourFilterTabContent; }
            set { armourFilterTabContent = value; RaisePropertyChanged("ArmourFilterTabContent"); }
        }

        private bool socketFilterEnabled;
        public bool SocketFilterEnabled
        {
            get { return socketFilterEnabled; }
            set { socketFilterEnabled = value; RaisePropertyChanged("SocketFilterEnabled"); }
        }

        private object socketFilterTabContent;
        public object SocketFilterTabContent
        {
            get { return socketFilterTabContent; }
            set { socketFilterTabContent = value; RaisePropertyChanged("SocketFilterTabContent"); }
        }

        private bool requirementFilterEnabled;
        public bool RequirementFilterEnabled
        {
            get { return requirementFilterEnabled; }
            set { requirementFilterEnabled = value; RaisePropertyChanged("RequirementFilterEnabled"); }
        }

        private object requirementFilterTabContent;
        public object RequirementFilterTabContent
        {
            get { return requirementFilterTabContent; }
            set { requirementFilterTabContent = value; RaisePropertyChanged("RequirementFilterTabContent"); }
        }

        private bool mapFilterEnabled;
        public bool MapFilterEnabled
        {
            get { return mapFilterEnabled; }
            set { mapFilterEnabled = value; RaisePropertyChanged("MapFilterEnabled"); }
        }

        private object mapFilterTabContent;
        public object MapFilterTabContent
        {
            get { return mapFilterTabContent; }
            set { mapFilterTabContent = value; RaisePropertyChanged("MapFilterTabContent"); }
        }

        private bool miscellaneousFilterEnabled;
        public bool MiscellaneousFilterEnabled
        {
            get { return miscellaneousFilterEnabled; }
            set { miscellaneousFilterEnabled = value; RaisePropertyChanged("MiscellaneousFilterEnabled"); }
        }

        private object miscellaneousFilterTabContent;
        public object MiscellaneousFilterTabContent
        {
            get { return miscellaneousFilterTabContent; }
            set { miscellaneousFilterTabContent = value; RaisePropertyChanged("MiscellaneousFilterTabContent"); }
        }
        #endregion filterTabs 

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
                    if (ItemTypeFilterTabContent == null) ItemTypeFilterTabContent = new ItemTypeFilter(this);
                    ((ItemTypeFilterTabContent as ItemTypeFilter).DataContext as ItemTypeFilterControl).FilterEnabled = true;
                    break;
                case 2:
                    if (WeaponFilterTabContent == null) WeaponFilterTabContent = new WeaponFilter(this);
                    ((WeaponFilterTabContent as WeaponFilter).DataContext as WeaponFilterControl).FilterEnabled = true;
                    break;
                case 3:
                    if (ArmourFilterTabContent == null) ArmourFilterTabContent = new ArmourFilter(this);
                    ((ArmourFilterTabContent as ArmourFilter).DataContext as ArmourFilterControl).FilterEnabled = true;
                    break;
                case 4:
                    if (SocketFilterTabContent == null) SocketFilterTabContent = new SocketFilter(this);
                    ((SocketFilterTabContent as SocketFilter).DataContext as SocketFilterControl).FilterEnabled = true;
                    break;
                case 5:
                    if (RequirementFilterTabContent == null) RequirementFilterTabContent = new RequirementsFilter(this);
                    ((RequirementFilterTabContent as RequirementsFilter).DataContext as RequirementsFilterControl).FilterEnabled = true;
                    break;
                case 6:
                    if (MapFilterTabContent == null) MapFilterTabContent = new MapFilter(this);
                    ((MapFilterTabContent as MapFilter).DataContext as MapFilterControl).FilterEnabled = true;
                    break;
                case 7:
                    if (MiscellaneousFilterTabContent == null) MiscellaneousFilterTabContent = new MiscellaneousFilter(this);
                    ((MiscellaneousFilterTabContent as MiscellaneousFilter).DataContext as MiscellaneousFilterControl).FilterEnabled = true;
                    break;
            }
        }

        private dynamic AddTabFilters()
        {
            return new
            {
                type_filters = ItemTypeFilterEnabled ? ((ItemTypeFilterControl)((ItemTypeFilter)ItemTypeFilterTabContent).DataContext).GetFilter() : null,
                weapon_filters = WeaponFilterEnabled ? ((WeaponFilterControl)((WeaponFilter)WeaponFilterTabContent).DataContext).GetFilter() : null,
                armour_filters = ArmourFilterEnabled ? ((ArmourFilterControl)((ArmourFilter)ArmourFilterTabContent).DataContext).GetFilter() : null,
                socket_filters = SocketFilterEnabled ? ((SocketFilterControl)((SocketFilter)SocketFilterTabContent).DataContext).GetFilter() : null,
                req_filters = RequirementFilterEnabled ? ((RequirementsFilterControl)((RequirementsFilter)RequirementFilterTabContent).DataContext).GetFilter() : null,
                map_filters = MapFilterEnabled ? ((MapFilterControl)((MapFilter)MapFilterTabContent).DataContext).GetFilter() : null,
                misc_filters = MiscellaneousFilterEnabled ? ((MiscellaneousFilterControl)((MiscellaneousFilter)MiscellaneousFilterTabContent).DataContext).GetFilter() : null
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
