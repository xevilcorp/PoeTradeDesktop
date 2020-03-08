using PoeTradeDesktop.Schemes.Searching._SearchResultItem;
using PoeTradeDesktop.UI.Components.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemListingFeatures : UserControl
    {
        public Listing Listing
        {
            get { return (Listing)GetValue(ListingProperty); }
            set { SetValue(ListingProperty, value); }
        }
        public static readonly DependencyProperty ListingProperty = DependencyProperty.Register("Listing", typeof(Listing), typeof(ItemListingFeatures));

        public ItemListingFeatures()
        {
            InitializeComponent();
            Loaded += ItemListingFeaturesLoaded;
        }

        private void ItemListingFeaturesLoaded(object sender, RoutedEventArgs e)
        {
            WrapPanel wrap = new WrapPanel();
            wrap.Margin = new Thickness(7, 3, 3, 3);
            if (Listing.Account.Online != null)
            {
                wrap.MouseEnter += PlayerNameMouseEnter;
                wrap.MouseLeave += PlayerNameMouseLeave;
            }

            Ellipse status = new Ellipse();
            status.Height = 7;
            status.Width = 7;
            if (Listing.Account.Online == null) status.Fill = UICollor.red;
            else if (Listing.Account.Online.Status == null) status.Fill = UICollor.green;
            else status.Fill = UICollor.orange;

            TextBlock playerName = new TextBlock();
            playerName.Margin = new Thickness(5, 0, 0, 0);
            playerName.FontFamily = (FontFamily)FindResource("SmallCaps");
            playerName.Foreground = UICollor.yellowGray;
            playerName.VerticalAlignment = VerticalAlignment.Center;
            playerName.Text = Listing.Account.Name.ToLower();

            Grid gridButtons = new Grid();
            gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Pixel) });
            gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
           
            Button btnWhisper = new Button();
            btnWhisper.Content = "WHISPER";
            btnWhisper.FontSize = 10;
            btnWhisper.BorderBrush = new SolidColorBrush(Color.FromArgb(0x10, 0xff, 0xff, 0xff));
            btnWhisper.Background = new SolidColorBrush(Color.FromArgb(0x10, 0xff, 0xff, 0xff));
            btnWhisper.Foreground = Brushes.White;
            btnWhisper.Style = (Style)FindResource("cleanButtonWithBorder");
            gridButtons.Children.Add(btnWhisper);
            btnWhisper.SetValue(Grid.ColumnProperty, 0);
            
            Button btnPM = new Button();
            btnPM.Content = "PM";
            btnPM.FontSize = 10;
            btnPM.BorderBrush = new SolidColorBrush(Color.FromArgb(0x10, 0xff, 0xff, 0xff));
            btnPM.Background = new SolidColorBrush(Color.FromArgb(0x10, 0xff, 0xff, 0xff));
            btnPM.Foreground = Brushes.White;
            btnPM.Style = (Style)FindResource("cleanButtonWithBorder");
            gridButtons.Children.Add(btnPM);
            btnPM.SetValue(Grid.ColumnProperty, 2);

            wrap.Children.Add(status);
            wrap.Children.Add(playerName);
            
            panel.Children.Add(wrap);
            panel.Children.Add(gridButtons);
        }

        private void PlayerNameMouseEnter(object sender, MouseEventArgs e)
        {
            WrapPanel wp = (WrapPanel)sender;
            wp.ToolTip = new ToolTip
            {
                Template = (ControlTemplate)FindResource("modToolTip"),
                Content = new TextBlock
                {
                    Text = Listing.Account.Online.League,
                    Foreground = Brushes.White
                }
            };
            ToolTipService.SetPlacement(wp, PlacementMode.Top);
            ToolTipService.SetShowDuration(wp, 10000);
            ToolTipService.SetVerticalOffset(wp, -2);
            ToolTipService.SetInitialShowDelay(wp, 0);
        }

        private void PlayerNameMouseLeave(object sender, MouseEventArgs e)
        {
            WrapPanel wp = (WrapPanel)sender;
            wp.ToolTip = null;
        }
    }
}
