using System.Windows.Controls;

namespace PoeTradeDesktop.UI.Components.FilterTabs
{

    public partial class ItemTypeFilter : UserControl
    {
        public ItemTypeFilter(object parent)
        {
            InitializeComponent();
            DataContext = new Controllers.FilterTabs.ItemTypeFilterControl(parent);
        }

    }
}
