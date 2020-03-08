using PoeTradeDesktop.Schemes.Enums;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using PoeTradeDesktop.UI.Components.Generic;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemProperties : UserControl
    {
        public List<Property> Source
        {
            get { return (List<Property>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public ItemProperties()
        {
            InitializeComponent();
            Loaded += ItemPropertiesLoaded;
        }

        private void ItemPropertiesLoaded(object sender, RoutedEventArgs e)
        {
            foreach (Property prop in Source)
            {
                TextBlock tb = new TextBlock();
                tb.Foreground = UICollor.gray;
                tb.FontFamily = (FontFamily)FindResource("SmallCaps");
                tb.TextAlignment = TextAlignment.Center;
                tb.TextTrimming = TextTrimming.CharacterEllipsis;
                tb.SetBinding(WidthProperty, new Binding { Path = new PropertyPath("ActualWidth"), Source = widthGrid });

                if (prop.DisplayMode == 0)
                {
                    tb.Inlines.Add(prop.Name);
                    if (prop.Values.Count != 0)
                    {
                        tb.Inlines.Add(": ");
                        tb.Inlines.Add(GetValue(prop.Values[0]));
                    }
                    if(Enum.IsDefined(typeof(PropertyType), prop.Type))
                    {
                        tb.MouseEnter += TextBlockMouseEnter;
                        tb.MouseLeave += TextBlockMouseLeave;
                    }
                }

                if (prop.DisplayMode == 1)
                {
                    tb.Inlines.Add(GetValue(prop.Values[0]));
                    tb.Inlines.Add(": ");
                    tb.Inlines.Add(prop.Name);
                }

                if (prop.DisplayMode == 3)
                {
                    string[] words = prop.Name.Split(' ');
                    string namePart = "";
                    byte count = 0;
                    foreach (string word in words)
                    {
                        if ("%" + count.ToString() == word)
                        {
                            tb.Inlines.Add(namePart);
                            tb.Inlines.Add(GetValue(prop.Values[count]));
                            namePart = ""; count++;
                        }
                        else
                        {
                            namePart += word + ' ';
                        }
                    }
                    if(namePart != "") tb.Inlines.Add(namePart);
                }

                panel.Children.Add(tb);
            }
        }

        private void TextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.Background = UICollor.lightGray;
            tb.Cursor = Cursors.Hand;
        }

        private void TextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.Background = Brushes.Transparent;
        }

        private Run GetValue(List<string> value)
        {
            return new Run { Text = value[0] + ' ', Foreground = GetColor((ValueStyle)Convert.ToByte(value[1])) };
        }

        public Brush GetColor(ValueStyle v)
        {
            switch (v)
            {
                case ValueStyle.Default: return Brushes.White;
                case ValueStyle.Augmented: return UICollor.purple;
                case ValueStyle.Unmet: return UICollor.red;
                case ValueStyle.PhysicalDamage: return Brushes.White;
                case ValueStyle.FireDamage: return UICollor.darkRed;
                case ValueStyle.ColdDamage: return UICollor.saturatedBlue;
                case ValueStyle.LightningDamage: return UICollor.yellow;
                case ValueStyle.ChaosDamage: return UICollor.pink;
                case ValueStyle.MagicItem: return UICollor.purple;
                case ValueStyle.RareItem: return UICollor.lightYellow;
                case ValueStyle.UniqueItem: return UICollor.darkOrange;
            }
            return null;
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(List<Property>), typeof(ItemProperties));
    }
}
