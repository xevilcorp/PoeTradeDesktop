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
    public partial class ItemDescription : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public int SeparatorType
        {
            get { return (int)GetValue(SeparatorTypeProperty); }
            set { SetValue(SeparatorTypeProperty, value); }
        }

        public ItemDescription()
        {
            Loaded += ItemDescriptionLoaded;
        }

        private void ItemDescriptionLoaded(object sender, RoutedEventArgs e)
        {
            if (Text == "") panel.Children.Clear();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ItemDescription));
        public static readonly DependencyProperty SeparatorTypeProperty = DependencyProperty.Register("SeparatorType", typeof(int), typeof(ItemDescription));

    }
}
