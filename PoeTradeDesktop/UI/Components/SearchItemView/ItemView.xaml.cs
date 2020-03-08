using PoeTradeDesktop.Schemes.Enums;
using PoeTradeDesktop.Schemes.Searching._SearchResultItem;
using PoeTradeDesktop.UI.Components.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemView : UserControl
    {
        public Listing Listing
        {
            get { return (Listing)GetValue(ListingProperty); }
            set { SetValue(ListingProperty, value); }
        }

        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public ItemView()
        {
            InitializeComponent();
            Loaded += ItemViewLoaded;
        }

        public void ItemViewLoaded(object sender, RoutedEventArgs e)
        {
            if (Item.Properties != null)
            {
                AddItem(new ItemProperties { Source = Item.Properties }, 0);
            }
            if (Item.UtilityMods != null)
            {
                AddItem(new ItemMods { Mods = Item.UtilityMods, Type = ModTypes.Utility, HasModInfo = false}, 0);
            }
            if (Item.ILvl != 0 || Item.Requirements != null)
            {
                AddItem(new ItemRequirements { RequirementSource = Item.Requirements, ItemLevel = Item.ILvl }, 1);
            }
            if (Item.DescrText != null)
            {
                AddItem(new ItemDescription { Text = Item.DescrText }, 2);
            }
            if (Item.EnchantMods != null)
            {
                AddItem(new ItemMods { Mods = Item.EnchantMods, Extended = Item.Extended, Type = ModTypes.Enchanted, HasModInfo = true }, 3);
            }
            if (Item.ImplicitMods != null)
            {
                AddItem(new ItemMods { Mods = Item.ImplicitMods, Extended = Item.Extended, Type = ModTypes.Implicit, HasModInfo = false }, 4);
            }
            if (Item.ExplicitMods != null)
            {
                AddItem(new ItemMods { Mods = Item.ExplicitMods, Extended = Item.Extended, Type = ModTypes.Explict, HasModInfo = true },5);
            }
            if (Item.CraftedMods != null)
            {
                AddItem(new ItemMods { Mods = Item.CraftedMods, Extended = Item.Extended, Type = ModTypes.Crafted, HasModInfo = true },5);
            }
            if(Item.Hybrid != null)
            {
                AddItem(new ItemHybridTitle { Title = Item.Hybrid.BaseTypeName }, 6);
                AddItem(new ItemProperties { Source = Item.Hybrid.Properties }, 7);
                AddItem(new ItemDescription { Text = Item.Hybrid.SecDescrText }, 8);
                AddItem(new ItemMods { Mods = Item.Hybrid.ExplicitMods, Type = ModTypes.Explict, HasModInfo = false }, 5);
            }
            if(Item.Corrupted)
            {
                AddItem(new TextBlock { Text = "Corrupted", Foreground = UICollor.red, FontSize = 13, FontFamily = (FontFamily)FindResource("SmallCaps"), TextAlignment = TextAlignment.Center }, 5);
            }
            if (!Item.Identified)
            {
                AddItem(new TextBlock { Text = "Unidentified", Foreground = UICollor.red, FontSize = 13, FontFamily = (FontFamily)FindResource("SmallCaps"), TextAlignment = TextAlignment.Center }, 5);
            }
            if (Item.AdditionalProperties != null)
            {
                AddItem(new ItemAdditionalProperties { Props = Item.AdditionalProperties }, 9);
            }
            if (Item.Extended.Dps + Item.Extended.Pdps + Item.Extended.Edps != 0)
            {
                AddItem(new ItemStatsBar { Extended = Item.Extended, Mode = 0 }, 10);

            }
            else if (Item.Extended.Ar + Item.Extended.Ev + Item.Extended.Es != 0)
            {
                AddItem(new ItemStatsBar { Extended = Item.Extended, Mode = 1}, 10);
            }
            //AddItem(new TextBlock { Text = Item.Note, Foreground = UICollor.yellowGray, TextAlignment = TextAlignment.Center, FontFamily = (FontFamily)FindResource("SmallCaps"), FontSize=11 }, 11);
        }

        byte lastGroup = 0;
        bool firstAdded = true;
        public void AddItem(UIElement element, byte group)
        {
            if (group != lastGroup && !firstAdded)
            {
                panel.Children.Add(new ItemSeparator { SeparatorType = Item.FrameType });
                lastGroup = group;
            }
            firstAdded = false;
            panel.Children.Add(element);
            
        }

        public static readonly DependencyProperty ListingProperty = DependencyProperty.Register("Listing", typeof(Item), typeof(ItemView));
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(Item), typeof(ItemView));
    }
}
