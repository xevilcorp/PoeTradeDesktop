using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System.Windows;
using System.Windows.Controls;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemHybridTitle : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ItemHybridTitle()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ItemHybridTitle));
    }
}
