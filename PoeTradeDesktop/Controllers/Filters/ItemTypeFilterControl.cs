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

        private SearchControl Parent;

        public ItemTypeFilterControl(object parent)
        {
            Parent = parent as SearchControl;
            HideContentCMD = new RelayCommand(HideContent);
        }

        public void HideContent(object o)
        {
            Parent.IsFilterContentHidden = true;
        }
    }
}
