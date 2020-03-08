using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using PoeTradeDesktop.UI.Components.Generic;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    public partial class ItemRequirements : UserControl
    {
        public List<Requirement> RequirementSource
        {
            get { return (List<Requirement>)GetValue(RequirementSourceProperty); }
            set { SetValue(RequirementSourceProperty, value); }
        }

        public int ItemLevel
        {
            get { return (int)GetValue(ItemLevelProperty); }
            set { SetValue(ItemLevelProperty, value); }
        }

        public ItemRequirements()
        {
            InitializeComponent();
            Loaded += RequirementsLoaded;
        }

        private void RequirementsLoaded(object sender, RoutedEventArgs e)
        {
            if(ItemLevel == 0 && RequirementSource == null)
            {
                ((Panel)this.Parent).Children.Remove(this);
            }
            else
            {
                if (ItemLevel == 0)
                {
                    panel.Children.Remove(txtItemLevel);
                }
                if (RequirementSource != null)
                {
                    int count = 1;
                    foreach (Requirement req in RequirementSource)
                    {
                        if (count > 1)
                        {
                            txtRequirements.Inlines.Add(new Run { Text = ", ", Foreground = UICollor.gray, FontSize = 12 });
                        }
                        AddRequirementToTextBox(req, txtRequirements);

                        count++;
                    }
                }
                else
                {
                    panel.Children.Remove(txtRequirements);
                }
            }
        }

        private void AddRequirementToTextBox(Requirement req, TextBlock tb)
        {
            Run r;

            switch (req.DisplayMode)
            {
                case 0:
                    r = new Run();
                    r.FontSize = 12;
                    r.Text = req.Name + " ";
                    r.Foreground = UICollor.gray;
                    tb.Inlines.Add(r);

                    r = new Run();
                    r.FontSize = 12;
                    r.Text = req.Values[0][0];
                    r.Foreground = Brushes.White;
                    tb.Inlines.Add(r);
                    break;
                case 1:
                    r = new Run();
                    r.FontSize = 12;
                    r.Text = req.Values[0][0] + " ";
                    r.Foreground = Brushes.White;
                    tb.Inlines.Add(r);

                    r = new Run();
                    r.FontSize = 12;
                    r.Text = req.Name;
                    r.Foreground = UICollor.gray;
                    tb.Inlines.Add(r);
                    break;
            }
        }

        public static readonly DependencyProperty RequirementSourceProperty = DependencyProperty.Register("RequirementSource", typeof(List<Requirement>), typeof(ItemRequirements));
        public static readonly DependencyProperty ItemLevelProperty = DependencyProperty.Register("ItemLevel", typeof(int), typeof(ItemRequirements));

    }
}
