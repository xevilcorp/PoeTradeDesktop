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

        Brush gray = new SolidColorBrush(Color.FromRgb(0x7F, 0x7F, 0x7F));
        Brush darkGray = new SolidColorBrush(Color.FromRgb(0x1D, 0x1C, 0x1C));
        Brush purple = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0xFF));

        private void StatsBarLoaded(object sender, RoutedEventArgs e)
        {
            if (Extended.Dps != 0) CreateTextBlock("DPS: ", Extended.Dps, Extended.Dps_aug, 0);
            if (Extended.Pdps != 0) CreateTextBlock("Physical DPS: ", Extended.Pdps, Extended.Pdps_aug, 1);
            if (Extended.Edps != 0) CreateTextBlock("Elemental DPS: ", Extended.Edps, Extended.Edps_aug, 2);

            if (Extended.Ar != 0) CreateTextBlock("Armour: ", Extended.Ar, Extended.Ar_aug, 0);
            if (Extended.Ev != 0) CreateTextBlock("Evasion: ", Extended.Ev, Extended.Ev_aug, 1);
            if (Extended.Es != 0) CreateTextBlock("Energy Shield: ", Extended.Es, Extended.Es_aug, 2);
        }

        private void CreateTextBlock(string name, int value, bool aug, int pos)
        {
            TextBlock tb = new TextBlock();
            Run r = new Run();
            r.Text = name;
            r.Foreground = gray;
            tb.Inlines.Add(r);

            Run r2 = new Run();
            r2.Text = value.ToString();
            r2.Foreground = aug ? purple : Brushes.White;
            tb.Inlines.Add(r2);

            tb.TextAlignment = TextAlignment.Center;
            tb.MouseEnter += TextBlockMouseEnter;
            tb.MouseLeave += TextBlockMouseLeave;


            panel.Children.Add(tb);

            tb.SetValue(Grid.ColumnProperty, pos);
        }

        public void TextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = ((TextBlock)sender);
            tb.Background = Brushes.Transparent ;
        }

        public void TextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = ((TextBlock)sender);
            tb.Background = darkGray;
            tb.Cursor = Cursors.Hand;
        }

        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(StatsBar));

    }
}
