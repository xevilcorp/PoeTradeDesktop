using PoeTradeDesktop.Schemes.Filtering;
using System.Collections.Generic;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.Filters
{
    class ItemTypeFilterControl : BaseControl
    {
        public ICommand HideContentCMD { get; set; }

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.ItemTypeFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }

        private List<ItemCategory> categories;
        public List<ItemCategory> Categories
        {
            get { return categories; }
            set { categories = value; RaisePropertyChanged("Categories"); } 
        }

        private ItemCategory selectedCategory;
        public ItemCategory SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value; RaisePropertyChanged("SelectedCategory"); }
        }

        private SearchControl Parent;

        public ItemTypeFilterControl(object parent)
        {
            Parent = parent as SearchControl;
            HideContentCMD = new RelayCommand(HideContent);

            LoadSources();
        }

        public async void LoadSources()
        {
            Categories = await ItemCategory.LoadAsync();
        }

        public void HideContent(object o)
        {
            Parent.IsFilterContentHidden = true;
        }
    }
}
