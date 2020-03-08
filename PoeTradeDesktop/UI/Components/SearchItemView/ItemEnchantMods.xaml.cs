using Newtonsoft.Json.Linq;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended._Mods;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.Windows.Data;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemEnchantMods : UserControl
    {
        public List<string> ImplicitMods
        {
            get { return (List<string>)GetValue(ImplicitModsProperty); }
            set { SetValue(ImplicitModsProperty, value); }
        }

        public Extended Extended
        {
            get { return (Extended)GetValue(ExtendedProperty); }
            set { SetValue(ExtendedProperty, value); }
        }

        public ItemEnchantMods()
        {
            InitializeComponent();
            Loaded += ItemImplicitModsLoaded;
        }

        private void ItemImplicitModsLoaded(object sender, RoutedEventArgs e)
        {
            Brush lightPurple = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0xFF));
            foreach (string mod in ImplicitMods)
            {
                TextBlock tb = new TextBlock();
                tb.Foreground = lightPurple;
                tb.TextAlignment = TextAlignment.Center;
                tb.TextTrimming = TextTrimming.CharacterEllipsis;
                tb.FontFamily = (FontFamily)FindResource("SmallCaps");
                tb.Text = mod;
                tb.MouseEnter += TextBlockMouseEnter;
                tb.MouseLeave += TextBlockMouseLeave;

                tb.SetBinding(WidthProperty, new Binding { Path = new PropertyPath("ActualWidth"), Source = this });

                panel.Children.Add(tb);
            }
        }

        private void TextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.Background = new SolidColorBrush(Color.FromArgb(0x10, 0xFF, 0xFF, 0xFF));
            tb.Cursor = Cursors.Hand;
            tb.ToolTip = GetModToolTip(tb.Text);
            ToolTipService.SetPlacement(tb, PlacementMode.Top);
            ToolTipService.SetShowDuration(tb, 10000);
            ToolTipService.SetVerticalOffset(tb, -15);
            ToolTipService.SetInitialShowDelay(tb, 0);
        }

        private void TextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.Background = Brushes.Transparent;
            tb.ToolTip = null;
        }

        public ToolTip GetModToolTip(string text)
        {
            ToolTip tt = new ToolTip();
            tt.Template = (ControlTemplate)FindResource("modToolTip");

            StackPanel container = new StackPanel();

            StackPanel panel = new StackPanel();

            List<ModDetails> modsDetails = Extended.Mods.Implicit;
            List<List<object>> hashesMods = Extended.Hashes.Implicit;
            List<string> mods = ImplicitMods;
            int modIndex = mods.IndexOf(text);
            string modId = hashesMods[modIndex][0].ToString();
            List<int> modDetaisIndexes = JArrayToIntList(hashesMods[modIndex][1]);

            foreach (int modInfoIndex in modDetaisIndexes)
            {
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Left;

                tb.Inlines.AddRange(GetMagnitude(modsDetails, modInfoIndex, modId));

                panel.Children.Add(tb);
            }

            container.Children.Add(panel);

            container.Children.Add(new Rectangle
            {
                Height = 1,
                Margin = new Thickness(0, 4, 0, 4),
                Fill = new SolidColorBrush(Color.FromRgb(0x55, 0x55, 0x55))
            });

            container.Children.Add(new TextBlock
            {
                FontStyle = FontStyles.Italic,
                Text = text,
                Foreground = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0xFF)),
                TextAlignment = TextAlignment.Justify,
                TextWrapping = TextWrapping.Wrap
            });

            tt.Content = container;
            return tt;
        }

        private List<int> JArrayToIntList(object jArray)
        {
            List<int> ints = new List<int>();
            foreach (JValue jValue in (JArray)jArray)
            {
                ints.Add((int)jValue);
            }
            return ints;
        }

        private List<Run> GetMagnitude(List<ModDetails> modsDetails, int modInfoIndex, string modId)
        {
            List<Magnitude> modMagnitudes = modsDetails[modInfoIndex].Magnitudes.FindAll(x => x.Hash == modId); ;

            List<Run> rs = new List<Run>();
            rs.Add(new Run { Text = "MAGNITUDE: ", Foreground = Brushes.White, FontSize = 11 });

            string modMagnitudeStr = "";
            for (int k = 0; k < modMagnitudes.Count; k++)
            {
                if (k > 0) modMagnitudeStr += " to ";

                Magnitude modMagnitude = modMagnitudes[k];
                if (modMagnitude.Min != modMagnitude.Max) modMagnitudeStr += modMagnitude.Min.ToString() + "-" + modMagnitude.Max.ToString();
                else modMagnitudeStr += modMagnitude.Min.ToString();

            }
            rs.Add(new Run { Text = "[" + modMagnitudeStr + "]", Foreground = Brushes.Gold });
            return rs;
        }

        public static readonly DependencyProperty ImplicitModsProperty = DependencyProperty.Register("ImplicitMods", typeof(List<string>), typeof(ItemImplicitMods));
        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(ItemImplicitMods));

    }
}
