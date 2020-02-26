using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
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

    public partial class StatsBar : UserControl
    {
        public Extended Extended
        {
            get { return (Extended)GetValue(ExtendedProperty); }
            set { SetValue(ExtendedProperty, value); }
        }

        public StatsBar()
        {
            InitializeComponent();
            Loaded += StatsBarLoaded;
        }

        private void StatsBarLoaded(object sender, RoutedEventArgs e)
        {

        }

        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(StatsBar));

    }
}
