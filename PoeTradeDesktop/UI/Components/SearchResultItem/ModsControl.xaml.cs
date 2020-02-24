using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

        public static readonly DependencyProperty ExplicitModsProperty = DependencyProperty.Register("ExplicitMods", typeof(List<string>), typeof(ModsControl));
        public static readonly DependencyProperty ImplicitModsProperty = DependencyProperty.Register("ImplicitMods", typeof(List<string>), typeof(ModsControl));
        public static readonly DependencyProperty CraftedModsProperty = DependencyProperty.Register("CraftedMods", typeof(List<string>), typeof(ModsControl));
        public static readonly DependencyProperty ExtendedProperty = DependencyProperty.Register("Extended", typeof(Extended), typeof(ModsControl));
        public static readonly DependencyProperty SeparatorTypeProperty = DependencyProperty.Register("SeparatorType", typeof(int), typeof(ModsControl));

    }
}
