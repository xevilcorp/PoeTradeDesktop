using PoeTradeDesktop.UI.Components.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemDescription : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ItemDescription()
        {
            InitializeComponent();
            Loaded += ItemDescriptionLoaded;
        }

        private void ItemDescriptionLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = new TextBlock
            {
                TextTrimming = TextTrimming.CharacterEllipsis,
                TextWrapping = TextWrapping.Wrap,
                MaxHeight = 50,
                TextAlignment = TextAlignment.Center,
                Foreground = UICollor.cyan,
                Text = Text,
                FontFamily = (FontFamily)FindResource("SmallCaps")
            };
            tb.MouseEnter += TextBlockMouseEnter;
            tb.MouseLeave += TextBlockMouseLeave;
            tb.SetBinding(WidthProperty, new Binding { Path = new PropertyPath("ActualWidth"), Source = widthGrid });
            panel.Children.Add(tb);

        }

        protected void TextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.ToolTip = GetToolTip(tb.Text);
            ToolTipService.SetPlacement(tb, PlacementMode.Top);
            ToolTipService.SetShowDuration(tb, 10000);
            ToolTipService.SetVerticalOffset(tb, -15);
            ToolTipService.SetInitialShowDelay(tb, 0);
        }

        protected void TextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.ToolTip = null;
        }

        public ToolTip GetToolTip(string text)
        {
            ToolTip tooltip = new ToolTip { Template = (ControlTemplate)FindResource("modToolTip") };
            StackPanel container = new StackPanel();
            container.Children.Add(ToolTipDescription(text));
            tooltip.Content = container;
            return tooltip;
        }

        private UIElement ToolTipDescription(string text)
        {
            return new TextBlock
            {
                FontStyle = FontStyles.Italic,
                Text = text,
                Foreground = UICollor.cyan,
                TextAlignment = TextAlignment.Justify,
                TextWrapping = TextWrapping.Wrap
            };
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ItemDescription));
    }
}
