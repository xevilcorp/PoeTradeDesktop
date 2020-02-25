using Newtonsoft.Json.Linq;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
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

        private List<string>[] explicitModsUnited = null;

        private void ModsControlLoaded(object sender, RoutedEventArgs e)
        {
            //LoadExplicitModsUnited();
        }
        

        private void LoadExplicitModsUnited()
        {
           /* for (int i = 0; i < ExplicitMods.Length; i++)
            {
                explicitModsUnited[i][0] = ExplicitMods[i];
                explicitModsUnited[i][1] = Extended.Hashes.Explicit[i][0].ToString();
                int[] modInfos = (int[])Extended.Hashes.Explicit[i][1];
                List<string> tierPreviewList = new List<string>();

                for (int k = 0; k < modInfos.Length; k++)
                {
                    tierPreviewList.Add(Extended.Mods.Explicit[k].Tier);
                }
            }*/
        }


        private void ToolTip_Opened(object sender, RoutedEventArgs e)
        {
            ToolTip tp = (ToolTip)sender;
            int explicitModIndex = ExplicitMods.IndexOf(tp.DataContext.ToString());
            string explicitModId = Extended.Hashes.Explicit[explicitModIndex][0].ToString();
            List<int> modInfoIndexes = new List<int>();
            JArray ja = (JArray)Extended.Hashes.Explicit[explicitModIndex][1];
            foreach(JValue jv in ja) modInfoIndexes.Add((int)jv);
            StackPanel panel = ((((StackPanel)tp.Content).Children[0]) as StackPanel);
            panel.Children.Clear();

            foreach (int modInfoIndex in modInfoIndexes)
            {
                string modTierType = Extended.Mods.Explicit[modInfoIndex].Tier;
                string modName = Extended.Mods.Explicit[modInfoIndex].Name;
                List<Magnitude> modMagnitudes = Extended.Mods.Explicit[modInfoIndex].Magnitudes.FindAll(x => x.Hash == explicitModId); ;

                Brush lightblue = new SolidColorBrush(Color.FromRgb(0x7A, 0xAF, 0xF1));
                Brush lightred = new SolidColorBrush(Color.FromRgb(0xEC, 0x76, 0x76));
                Brush white50 = new SolidColorBrush(Color.FromArgb(0x4F, 0xFF, 0xFF, 0xFF));
                Brush tierColor = modTierType.IndexOf('S') != -1 ? lightblue : lightred;

                string modType = modTierType.IndexOf('S') != -1 ? "SULFIX" : "PREFIX";

                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Left;

                tb.Inlines.Add(new Run { Text = modType, Foreground = tierColor, FontSize = 11});
                if(modTierType != "" && modTierType.Length > 1)
                {
                    tb.Inlines.Add(new Run { Text = " | ", Foreground = white50 });
                    tb.Inlines.Add(new Run { Text = "TIER: ", Foreground = Brushes.White, FontSize = 11 });
                    tb.Inlines.Add(new Run { Text = modTierType.Substring(1, 1), Foreground = Brushes.White });
                }
             
                tb.Inlines.Add(new Run { Text = " | ", Foreground = white50 });
                tb.Inlines.Add(new Run { Text = "MAGNITUDE: ", Foreground = Brushes.White, FontSize = 11 });

                string modMagnitudeStr = "";
                for (int k = 0; k < modMagnitudes.Count; k++)
                {
                    if (k > 0) modMagnitudeStr += " TO ";

                    Magnitude modMagnitude = modMagnitudes[k];
                    modMagnitudeStr += modMagnitude.Min.ToString() + "-" + modMagnitude.Max.ToString();
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
        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(ModsControl));
        public static readonly DependencyProperty SeparatorTypeProperty = DependencyProperty.Register("SeparatorType", typeof(int), typeof(ModsControl));

    }
}
