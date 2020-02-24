using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PoeTradeDesktop.UI.Components.SearchResultItem
{
    public partial class Properties : UserControl
    {
        public List<Property> Source
        {
            get { return (List<Property>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Properties()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(List<Property>), typeof(Properties));


    }
}
