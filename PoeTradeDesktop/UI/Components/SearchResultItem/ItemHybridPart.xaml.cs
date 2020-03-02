using PoeTradeDesktop.Schemes.Searching._SearchResultItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoeTradeDesktop.UI.Components.SearchResultItem
{
    public partial class ItemHybridPart : UserControl
    {
        public int SeparatorType
        {
            get { return (int)GetValue(SeparatorTypeProperty); }
            set { SetValue(SeparatorTypeProperty, value); }
        }

        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }


        public ItemHybridPart()
        {
            Loaded += ItemHybridPartLoaded;
        }

        private void ItemHybridPartLoaded(object sender, RoutedEventArgs e)
        {
            if (Item != null) { InitializeComponent();  panel.Visibility = Visibility.Visible;} 
        }

        public static readonly DependencyProperty SeparatorTypeProperty = DependencyProperty.Register("SeparatorType", typeof(int), typeof(ItemHybridPart));
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(Item), typeof(ItemHybridPart));

    }
}
