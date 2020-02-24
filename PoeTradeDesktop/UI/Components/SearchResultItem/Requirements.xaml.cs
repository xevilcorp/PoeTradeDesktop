using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Components.SearchResultItem
{
    public partial class Requirements : UserControl
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

        public int SeparatorType
        {
            get { return (int)GetValue(SeparatorTypeProperty); }
            set { SetValue(SeparatorTypeProperty, value); }
        }


        Brush gray = new SolidColorBrush(Color.FromRgb(0x7F, 0x7F, 0x7F));
        Brush white = new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF));

        public Requirements()
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
                            txtRequirements.Inlines.Add(new Run { Text = ", ", Foreground = gray, FontSize = 12 });
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
                    r.Foreground = gray;
                    tb.Inlines.Add(r);

                    r = new Run();
                    r.FontSize = 12;
                    r.Text = req.Values[0][0];
                    r.Foreground = white;
                    tb.Inlines.Add(r);
                    break;
                case 1:
                    r = new Run();
                    r.FontSize = 12;
                    r.Text = req.Values[0][0] + " ";
                    r.Foreground = white;
                    tb.Inlines.Add(r);

                    r = new Run();
                    r.FontSize = 12;
                    r.Text = req.Name;
                    r.Foreground = gray;
                    tb.Inlines.Add(r);
                    break;
            }
        }

        public static readonly DependencyProperty RequirementSourceProperty = DependencyProperty.Register("RequirementSource", typeof(List<Requirement>), typeof(Requirements));
        public static readonly DependencyProperty ItemLevelProperty = DependencyProperty.Register("ItemLevel", typeof(int), typeof(Requirements));
        public static readonly DependencyProperty SeparatorTypeProperty = DependencyProperty.Register("SeparatorType", typeof(int), typeof(Requirements));

    }
}
