using Newtonsoft.Json.Linq;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item._Extended._Mods;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Components.SearchResultItem
{
    public partial class ModsControl : UserControl
    {
        public int SeparatorType
        {
            get { return (int)GetValue(SeparatorTypeProperty); }
            set { SetValue(SeparatorTypeProperty, value); }
        }

        public List<string> ExplicitMods
        {
            get { return (List<string>)GetValue(ExplicitModsProperty); }
            set { SetValue(ExplicitModsProperty, value); }
        }

        public List<string> ImplicitMods
        {
            get { return (List<string>)GetValue(ImplicitModsProperty); }
            set { SetValue(ImplicitModsProperty, value); }
        }

        public List<string> CraftedMods
        {
            get { return (List<string>)GetValue(CraftedModsProperty); }
            set { SetValue(CraftedModsProperty, value); }
        }

        public List<string> EnchantMods
        {
            get { return (List<string>)GetValue(EnchantModsProperty); }
            set { SetValue(EnchantModsProperty, value); }
        }

        public Extended Extended
        {
            get { return (Extended)GetValue(ExtendedProperty); }
            set { SetValue(ExtendedProperty, value); }
        }


        public ModsControl()
        {
            InitializeComponent();
            Loaded += ModsControlLoaded;
        }

        private void ModsControlLoaded(object sender, RoutedEventArgs e)
        {
            if (ExplicitMods == null) panel.Children.Remove(explicitList);
            if (ImplicitMods == null) panel.Children.Remove(implicitList);
            if (EnchantMods == null) panel.Children.Remove(enchantList);
            if (CraftedMods == null) panel.Children.Remove(craftedList);

            if (EnchantMods == null) panel.Children.Remove(separatorOne);
            if (ImplicitMods == null) panel.Children.Remove(separatorTwo);
            if (ExplicitMods == null && CraftedMods == null) panel.Children.Remove(separatorThree);
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

        private void ToolTipOpened(object sender, RoutedEventArgs e)
        {
            ToolTip toolTip = (ToolTip)sender;

            List<ModDetails> modsDetails= null;
            List<List<object>> hashesMods = null;
            List<string> mods = null;

            switch (toolTip.Tag.ToString())
            {
                case "Implicit":
                    modsDetails = Extended.Mods.Implicit;
                    hashesMods = Extended.Hashes.Implicit;
                    mods = ImplicitMods;
                    break;
                case "Explicit":
                    modsDetails = Extended.Mods.Explicit;
                    hashesMods = Extended.Hashes.Explicit;
                    mods = ExplicitMods;
                    break;
                case "Crafted":
                    modsDetails = Extended.Mods.Crafted;
                    hashesMods = Extended.Hashes.Crafted;
                    mods = CraftedMods;
                    break;
                case "Enchant":
                    modsDetails = Extended.Mods.Enchant;
                    hashesMods = Extended.Hashes.Enchant;
                    mods = EnchantMods;
                    break;
            }

            int modIndex = mods.IndexOf(toolTip.DataContext.ToString());

            string modId = hashesMods[modIndex][0].ToString();

            List<int> modDetaisIndexes = JArrayToIntList(hashesMods[modIndex][1]);

            StackPanel panel = ((((StackPanel)toolTip.Content).Children[0]) as StackPanel);
            panel.Children.Clear();

            foreach (int modInfoIndex in modDetaisIndexes)
            {
                string modTierType = modsDetails[modInfoIndex].Tier;
                string modName = modsDetails[modInfoIndex].Name;
                List<Magnitude> modMagnitudes = modsDetails[modInfoIndex].Magnitudes.FindAll(x => x.Hash == modId); ;

                Brush lightBlue = new SolidColorBrush(Color.FromRgb(0x7A, 0xAF, 0xF1));
                Brush lightPurple= new SolidColorBrush(Color.FromRgb(0x7A, 0xAF, 0xF1));
                Brush lightRed = new SolidColorBrush(Color.FromRgb(0xEC, 0x76, 0x76));
                Brush lowWhite = new SolidColorBrush(Color.FromArgb(0x4F, 0xFF, 0xFF, 0xFF));

                Brush tierColor = null; string modType = "";

                if (modTierType.IndexOf('S') != -1) { tierColor = lightPurple; modType = "SULFIX"; }
                else if ((modTierType.IndexOf('P') != -1)) { tierColor = lightRed; modType = "PREFIX"; }
                else if((modTierType.IndexOf('R') != -1)) { tierColor = lightBlue; modType = "CRAFTED"; }
                else if(toolTip.Tag.ToString() == "Enchant") { tierColor = lightPurple; modType = "ENCHANTED"; }
                else { tierColor = lightPurple; modType = ""; }

                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Left;

                if (modType != "")
                tb.Inlines.Add(new Run { Text = modType, Foreground = tierColor, FontSize = 11});

                if(modTierType != "" && modTierType.Length > 1)
                {
                    tb.Inlines.Add(new Run { Text = " | ", Foreground = lowWhite });
                    tb.Inlines.Add(new Run { Text = "TIER: ", Foreground = Brushes.White, FontSize = 11 });
                    tb.Inlines.Add(new Run { Text = modTierType.Substring(1, 1), Foreground = Brushes.White });
                }
             
                if(modType != "")
                tb.Inlines.Add(new Run { Text = " | ", Foreground = lowWhite });
                tb.Inlines.Add(new Run { Text = "MAGNITUDE: ", Foreground = Brushes.White, FontSize = 11 });
                string modMagnitudeStr = "";
                for (int k = 0; k < modMagnitudes.Count; k++)
                {
                    if (k > 0) modMagnitudeStr += " to ";

                    Magnitude modMagnitude = modMagnitudes[k];
                    if(modMagnitude.Min != modMagnitude.Max)
                    modMagnitudeStr += modMagnitude.Min.ToString() + "-" + modMagnitude.Max.ToString();
                    else 
                    modMagnitudeStr += modMagnitude.Min.ToString();

                }
                tb.Inlines.Add(new Run { Text = "[" + modMagnitudeStr + "]", Foreground = Brushes.Gold });
                
                if(modName != "")
                {
                    tb.Inlines.Add(new Run { Text = " | ", Foreground = Brushes.White });
                    tb.Inlines.Add(new Run { Text = modName, Foreground = tierColor });
                }
               
                panel.Children.Add(tb);
            }
        }

        public static readonly DependencyProperty ExplicitModsProperty = DependencyProperty.Register("ExplicitMods", typeof(List<string>), typeof(ModsControl));
        public static readonly DependencyProperty ImplicitModsProperty = DependencyProperty.Register("ImplicitMods", typeof(List<string>), typeof(ModsControl));
        public static readonly DependencyProperty CraftedModsProperty = DependencyProperty.Register("CraftedMods", typeof(List<string>), typeof(ModsControl));
        public static readonly DependencyProperty EnchantModsProperty = DependencyProperty.Register("EnchantMods", typeof(List<string>), typeof(ModsControl));
        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(ModsControl));
        public static readonly DependencyProperty SeparatorTypeProperty = DependencyProperty.Register("SeparatorType", typeof(int), typeof(ModsControl));
    }
}
