using Newtonsoft.Json.Linq;
using PoeTradeDesktop.Schemes.Enums;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended._Mods;
using PoeTradeDesktop.UI.Components.Generic;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemMods : UserControl
    {
        #region Properties
        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(ItemMods));
        public static readonly DependencyProperty ModsProperty = DependencyProperty.Register("Mods", typeof(List<string>), typeof(ItemMods));
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ModTypes), typeof(ItemMods));
        public static readonly DependencyProperty HasModInfoProperty = DependencyProperty.Register("HasModInfo", typeof(bool), typeof(ItemMods));

        public ModTypes Type
        {
            get { return (ModTypes)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public Extended Extended
        {
            get { return (Extended)GetValue(ExtendedProperty); }
            set { SetValue(ExtendedProperty, value); }
        }

        public List<string> Mods
        {
            get { return (List<string>)GetValue(ModsProperty); }
            set { SetValue(ModsProperty, value); }
        }


        public bool HasModInfo
        {
            get { return (bool)GetValue(HasModInfoProperty); }
            set { SetValue(HasModInfoProperty, value); }
        }

        protected List<ModDetails> ModsDetails { get; set; }
        protected List<List<object>> HashesMods { get; set; }
        private Brush ModForegroundColor { get; set; }

        #endregion Properties

        public ItemMods()
        {
            InitializeComponent();
            Loaded += ItemModsLoaded;
        }

        #region Events

        public void Setup()
        {
            switch (Type)
            {
                case ModTypes.Implicit:
                    ModForegroundColor = UICollor.purple;
                    break;
                case ModTypes.Explict:
                    if (Extended != null)
                    {
                        ModsDetails = Extended.Mods != null ? Extended.Mods.Explicit : null;
                        HashesMods = Extended.Hashes != null ? Extended.Hashes.Explicit : null;
                    }
                    ModForegroundColor = UICollor.purple;
                    break;
                case ModTypes.Crafted:
                    if (Extended != null)
                    {
                        ModsDetails = Extended.Mods != null ? Extended.Mods.Crafted : null;
                        HashesMods = Extended.Hashes != null ? Extended.Hashes.Crafted : null;
                    }
                    ModForegroundColor = UICollor.lightBlue;
                    break;
                case ModTypes.Enchanted:
                    if (Extended != null)
                    {
                        ModsDetails = Extended.Mods != null ? Extended.Mods.Enchant : null;
                        HashesMods = Extended.Hashes != null ? Extended.Hashes.Enchant : null;
                    }
                    ModForegroundColor = UICollor.lightPurple;
                    break;
                case ModTypes.Utility:
                    ModForegroundColor = UICollor.purple;
                    break;
            }
        }

        private void ItemModsLoaded(object sender, RoutedEventArgs e)
        {
            Setup();
            if (ModsDetails == null || HashesMods == null) HasModInfo = false;

            foreach (string mod in Mods)
            {
                TextBlock tb = new TextBlock();
                tb.Foreground = ModForegroundColor;
                tb.TextAlignment = TextAlignment.Center;
                tb.TextTrimming = TextTrimming.CharacterEllipsis;
                tb.FontFamily = (FontFamily)FindResource("SmallCaps");
                tb.Text = mod;
                tb.MouseEnter += TextBlockMouseEnter;
                tb.MouseLeave += TextBlockMouseLeave;

                tb.SetBinding(WidthProperty, new Binding { Path = new PropertyPath("ActualWidth"), Source = widthGrid });

                stkPanel.Children.Add(tb);
            }

        }

        protected void TextBlockMouseEnter(object sender, MouseEventArgs e)
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

        protected void TextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            tb.Background = Brushes.Transparent;
            tb.ToolTip = null;
        }
        #endregion Events

        #region Functions

        private List<Run> GetMagnitude(List<ModDetails> ModsDetails, int modInfoIndex, string modId)
        {
            List<Magnitude> modMagnitudes = ModsDetails[modInfoIndex].Magnitudes.FindAll(x => x.Hash == modId); ;

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

        private List<Run> GetModName(string modName, Brush tierColor)
        {
            List<Run> rs = new List<Run>();

            rs.Add(new Run { Text = " | ", Foreground = Brushes.White });
            rs.Add(new Run { Text = modName, Foreground = tierColor });

            return rs;
        }

        private Run GetModType(string modType, Brush tierColor)
        {
            return new Run { Text = modType, Foreground = tierColor, FontSize = 11 };
        }

        private List<Run> GetModTier(string modTierType)
        {
            List<Run> rs = new List<Run>();
            if (modTierType != "" && modTierType.Length > 1)
            {
                rs.Add(new Run { Text = " | ", Foreground = UICollor.lowWhite });
                rs.Add(new Run { Text = "TIER: ", Foreground = Brushes.White, FontSize = 11 });
                rs.Add(new Run { Text = modTierType.Substring(1, 1), Foreground = Brushes.White });
                rs.Add(new Run { Text = " | ", Foreground = Brushes.White });

            }
            return rs;
        }

        private void SetTierColorModType(string modTierType, ref Brush tierColor, ref string modType)
        {
            if (modTierType.IndexOf('S') != -1) { tierColor = UICollor.lightPurple; modType = "SULFIX"; }
            else if ((modTierType.IndexOf('P') != -1)) { tierColor = UICollor.lightRed; modType = "PREFIX"; }
            else if (Type == ModTypes.Crafted) { tierColor = UICollor.lightBlue; modType = "CRAFTED"; }
            else if (Type == ModTypes.Enchanted) { tierColor = UICollor.lightPurple; modType = "ENCHANTED"; }
            else { tierColor = UICollor.lightPurple; modType = ""; }
        }

        private UIElement ModToolTipInfo(string text)
        {
            StackPanel panel = new StackPanel();

            int modIndex = Mods.IndexOf(text);
            string modId = HashesMods[modIndex][0].ToString();
            List<int> modDetaisIndexes = JArrayToIntList(HashesMods[modIndex][1]);

            foreach (int modInfoIndex in modDetaisIndexes)
            {
                TextBlock tb = new TextBlock { HorizontalAlignment = HorizontalAlignment.Left };

                string modTierType = ModsDetails[modInfoIndex].Tier;
                string modName = ModsDetails[modInfoIndex].Name;
                Brush tierColor = null; string modType = "";
                SetTierColorModType(modTierType, ref tierColor, ref modType);

                if (modType != "") tb.Inlines.Add(GetModType(modType, tierColor));
                if (modTierType != "" && modTierType.Length > 1) tb.Inlines.AddRange(GetModTier(modTierType));
                tb.Inlines.AddRange(GetMagnitude(ModsDetails, modInfoIndex, modId));
                if (modName != "") tb.Inlines.AddRange(GetModName(modName, tierColor));

                panel.Children.Add(tb);
            }

            return panel;
        }

        private UIElement ModToolTipSeparator()
        {
            return new Rectangle
            {
                Height = 1,
                Margin = new Thickness(0, 4, 0, 4),
                Fill = new SolidColorBrush(Color.FromRgb(0x55, 0x55, 0x55))
            };
        }

        private UIElement ModToolTipDescription(string text)
        {
            return new TextBlock
            {
                FontStyle = FontStyles.Italic,
                Text = text,
                Foreground = UICollor.purple,
                TextAlignment = TextAlignment.Justify,
                TextWrapping = TextWrapping.Wrap
            };
        }

        public ToolTip GetModToolTip(string text)
        {
            ToolTip tooltip = new ToolTip { Template = (ControlTemplate)FindResource("modToolTip") };

            StackPanel container = new StackPanel();

            if(HasModInfo)
            {
                container.Children.Add(ModToolTipInfo(text));

                container.Children.Add(ModToolTipSeparator());
            }

            container.Children.Add(ModToolTipDescription(text));

            tooltip.Content = container;
            return tooltip;
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

        #endregion Functions
    }
}
