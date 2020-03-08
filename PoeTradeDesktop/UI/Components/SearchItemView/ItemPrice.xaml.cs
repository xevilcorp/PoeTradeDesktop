using PoeTradeDesktop.Schemes;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Listing;
using PoeTradeDesktop.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PoeTradeDesktop.UI.Custom;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemPrice : UserControl
    {
        public Price Price
        {
            get { return (Price)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public ItemPrice()
        {
            InitializeComponent();
            Loaded += ItemPrice_Loaded;
        }

        private void ItemPrice_Loaded(object sender, RoutedEventArgs e)
        {
            Brush lightYellow = new SolidColorBrush(Color.FromRgb(0xAA, 0x9E, 0x82));
            Brush lightGold = new SolidColorBrush(Color.FromRgb(0xA3, 0x8D, 0x6D));
            
            string priceType = Price.Type == "~price" ? "Exact Price:" : "Asking Price:";
            TextBlock txtPriceType = new TextBlock { Text = priceType, Foreground = lightGold, TextAlignment = TextAlignment.Center, Margin = new Thickness(0,6,0,0)};

            WrapPanel wrap = new WrapPanel { HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0,0,0,3)};

            wrap.Children.Add(new TextBlock { Text = Price.Amount + "x ", Foreground = lightYellow, Margin = new Thickness(0,0,0,0), VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold});
            
            StaticInfo info = StaticInfo.Get(Price.Currency);
            string imagePath = "https://web.poecdn.com" + info.Image;
            wrap.Children.Add(new ImageBox { CacheSource = imagePath, Width = 26 });

            //wrap.Children.Add(new TextBlock { Text = " " + info.Text, Foreground = lightYellow });

            panel.Children.Add(txtPriceType);
            panel.Children.Add(wrap);
        }

        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(Price), typeof(ItemPrice));

    }
}
