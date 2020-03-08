using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemSeparator : UserControl
    {
        public int SeparatorType
        {
            get { return (int)GetValue(SeparatorTypeProperty); }
            set { SetValue(SeparatorTypeProperty, value); }
        }

        public ItemSeparator()
        {
            InitializeComponent();
            Loaded += ItemSeparatorLoaded;
        }

        private void ItemSeparatorLoaded(object sender, RoutedEventArgs e)
        {
            Image img = new Image
            {
                Margin = new Thickness(0,1,0,1),
                Source = new BitmapImage(new Uri($"../../Images/separator_{SeparatorType}.png", UriKind.Relative))
            };
            this.AddChild(img);
        }

        public static readonly DependencyProperty SeparatorTypeProperty = DependencyProperty.Register("SeparatorType", typeof(int), typeof(ItemDescription));

    }
}
