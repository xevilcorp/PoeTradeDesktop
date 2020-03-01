using System.Windows.Controls;

namespace PoeTradeDesktop.UI.Components.FilterTabs
{
    public partial class WeaponFilter : UserControl
    {
        public WeaponFilter(object parent)
        {
            InitializeComponent();
            DataContext = new Controllers.FilterTabs.WeaponFilterControl(parent);
        }
    }
}
