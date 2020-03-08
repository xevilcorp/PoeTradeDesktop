using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemAdditionalProperties : UserControl
    {
        #region Properties
        public List<Property> Props
        {
            get { return (List<Property>)GetValue(PropsProperty); }
            set { SetValue(PropsProperty, value); }
        }
        #endregion Properties

        public ItemAdditionalProperties()
        {
            InitializeComponent();
            Loaded += ItemAdditionalPropertiesLoaded;
        }

        private void ItemAdditionalPropertiesLoaded(object sender, RoutedEventArgs e)
        {
            foreach (Property prop in Props)
            {
                this.container.Children.Add(CreateControl(prop));
            }
        }

        public UIElement CreateControl(Property prop)
        {
            if (prop.Type == 20) return GenerateProgressBar(prop);
            return null;
        }

        public UIElement GenerateProgressBar(Property prop)
        {
            const int barMaxWidth = 140;
            const int barHeight = 10;
            double barWidth = barMaxWidth * prop.Progress;

            Border containerBorder = new Border();
            containerBorder.Cursor = Cursors.Hand;
            containerBorder.HorizontalAlignment = HorizontalAlignment.Center;
            containerBorder.Padding = new Thickness(2, 1, 2, 1);
            containerBorder.MouseEnter += ContainerBorderMouseEnter;
            containerBorder.MouseLeave += ContainerBorderMouseLeave;
            WrapPanel wrap = new WrapPanel();

            Border barBorder = new Border();
            barBorder.Padding = new Thickness(2, 1, 2, 1);
            barBorder.HorizontalAlignment = HorizontalAlignment.Center;
            barBorder.VerticalAlignment = VerticalAlignment.Center;
            barBorder.BorderThickness = new Thickness(2);
            barBorder.Width = barMaxWidth + 8;
            barBorder.Height = barHeight;
            barBorder.CornerRadius = new CornerRadius(2);
            barBorder.Background = new SolidColorBrush(Color.FromRgb(0x1C, 0x1F, 0x26));
            barBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(0x2B, 0x2F, 0x39));

            Rectangle bar = new Rectangle();
            bar.Fill = new SolidColorBrush(Color.FromRgb(0xD8, 0x96, 0x25));
            bar.RadiusX = 2; bar.RadiusY = 2;
            bar.Width = barWidth;
            bar.HorizontalAlignment = HorizontalAlignment.Left;

            barBorder.Child = bar;
            wrap.Children.Add(barBorder);

            TextBlock valueArea = new TextBlock();
            valueArea.Foreground = Brushes.White;
            valueArea.FontSize = 11;
            valueArea.VerticalAlignment = VerticalAlignment.Center;
            valueArea.Margin = new Thickness(5, 0, 0, 0);
            valueArea.Text = prop.Values[0][0];
            wrap.Children.Add(valueArea);

            containerBorder.Child = wrap;

            return containerBorder;
        }

        private void ContainerBorderMouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = Brushes.Transparent;
        }

        private void ContainerBorderMouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = new SolidColorBrush(Color.FromRgb(0x20, 0x1D, 0x1D));
        }


        public static readonly DependencyProperty PropsProperty = DependencyProperty.Register("Props", typeof(List<Property>), typeof(ItemAdditionalProperties));

    }
}
