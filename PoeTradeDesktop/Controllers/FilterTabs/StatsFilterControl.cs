using PoeTradeDesktop.Schemes.Filtering;
using System.Collections.Generic;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.FilterTabs
{
    public class StatsFilterControl : BaseControl
    {
        public ICommand HideContentCMD { get; set; }

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.StatsFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }

        private List<StatGroup> statsGroups;
        public List<StatGroup> StatsGroups
        {
            get { return statsGroups; }
            set { statsGroups = value; RaisePropertyChanged("StatsGroups"); }
        }

        private SearchControl Parent;

        public StatsFilterControl(object parent)
        {
            Parent = parent as SearchControl;
            HideContentCMD = new RelayCommand(HideContent);

            LoadSources();
        }

        public async void LoadSources()
        {
        }

        public dynamic GetFilter()
        {
            if (FilterEnabled)
            {
                return new
                {
                    filters = new
                    {
                       // category = SelectedCategory.Id,
                        //rarity = SelectedRarity.Id
                    }
                };
            }

            return null;
        }

        public void HideContent(object o)
        {
            Parent.IsFilterContentHidden = true;
        }
    }
}
