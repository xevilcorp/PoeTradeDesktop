using System.Windows.Controls;

namespace PoeTradeDesktop.UI.Components.FilterTabs
{
    public partial class StatsFilter : UserControl
    {
        public StatsFilter(object parent)
        {
            InitializeComponent();
            DataContext = new Controllers.FilterTabs.StatsFilterControl(parent);
        }
    }
}
