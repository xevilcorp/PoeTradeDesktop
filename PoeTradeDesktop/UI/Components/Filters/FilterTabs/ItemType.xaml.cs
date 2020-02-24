using System.Windows.Controls;

namespace PoeTradeDesktop.Interface.Filters
{

    public partial class ItemType : UserControl
    {
        public ItemType(object parent)
        {
            InitializeComponent();
            DataContext = new Controllers.Filters.ItemTypeFilterControl(parent);
        }

    }
}
