using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using PoeTradeDesktop.UI.Components.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{

    public partial class ItemStatsBar : UserControl
    {
        public Extended Extended
        {
            get { return (Extended)GetValue(ExtendedProperty); }
            set { SetValue(ExtendedProperty, value); }
        }

        public byte Mode
        {
            get { return (byte)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public ItemStatsBar()
        {
            InitializeComponent();
            Loaded += StatsBarLoaded;
        }

        private void StatsBarLoaded(object sender, RoutedEventArgs e)
        {
            if(Mode == 0)
            {
                if (Extended.Dps != 0) CreateTextBlock("DPS: ", Extended.Dps, Extended.Dps_aug, 0);
                if (Extended.Pdps != 0) CreateTextBlock("P.DPS: ", Extended.Pdps, Extended.Pdps_aug, 1);
                if (Extended.Edps != 0) CreateTextBlock("E.DPS: ", Extended.Edps, Extended.Edps_aug, 2);
            }
            else
            {
                if (Extended.Ar != 0) CreateTextBlock("AR: ", Extended.Ar, Extended.Ar_aug, 0);
                if (Extended.Ev != 0) CreateTextBlock("EV: ", Extended.Ev, Extended.Ev_aug, 1);
                if (Extended.Es != 0) CreateTextBlock("ES: ", Extended.Es, Extended.Es_aug, 2);
            }
          
        }

        private void CreateTextBlock(string name, float value, bool aug, int pos)
        {
            TextBlock tb = new TextBlock();
            Run r = new Run();
            r.Text = name;
            r.FontSize = 10;
            r.Foreground = UICollor.gray;
            tb.Inlines.Add(r);

            Run r2 = new Run();
            r2.Text = value.ToString();
            r2.FontSize = 11;
            r2.Foreground = aug ? UICollor.purple : Brushes.White;
            r2.FontFamily = new FontFamily("Segoe UI Light");
            tb.Inlines.Add(r2);

            tb.Padding = new Thickness(3, 1, 5, 0);

            tb.MouseEnter += TextBlockMouseEnter;
            tb.MouseLeave += TextBlockMouseLeave;

            panel.Children.Add(tb);
        }

        public void TextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = ((TextBlock)sender);
            tb.Background = Brushes.Transparent ;
        }

        public void TextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = ((TextBlock)sender);
            tb.Background = UICollor.darkGray;
            tb.Cursor = Cursors.Hand;
        }

        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(ItemStatsBar));
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(byte), typeof(ItemStatsBar));

    }
}
