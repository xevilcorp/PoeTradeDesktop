using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoeTradeDesktop.UI.Components.SearchResultItem
{
    /// <summary>
    /// Interaction logic for TitleFrame.xaml
    /// </summary>
    public partial class TitleFrame : UserControl
    {

        public int ItemFrameType
        {
            get { return (int)GetValue(ItemFrameTypeProperty); }
            set { SetValue(ItemFrameTypeProperty, value); }
        }

        public string ItemName
        {
            get { return (string)GetValue(ItemNameProperty); }
            set { SetValue(ItemNameProperty, value); }
        }

        public string ItemTypeLine
        {
            get { return (string)GetValue(ItemTypeLineProperty); }
            set { SetValue(ItemTypeLineProperty, value); }
        }
        
        public TitleFrame()
        {
            InitializeComponent();
            _ = ItemFrameType;

        }

        private void TitleFrameLoaded(object sender, RoutedEventArgs e)
        {
            _ = ItemFrameType;
        }


        public static readonly DependencyProperty ItemFrameTypeProperty = DependencyProperty.Register("ItemFrameType", typeof(int), typeof(TitleFrame));
        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register("ItemName", typeof(string), typeof(TitleFrame));
        public static readonly DependencyProperty ItemTypeLineProperty = DependencyProperty.Register("ItemTypeLine", typeof(string), typeof(TitleFrame));
    }
}
