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

namespace PoeTradeDesktop.UI.Components.FilterTabs
{
    /// <summary>
    /// Interaction logic for ArmourFilter.xaml
    /// </summary>
    public partial class ArmourFilter : UserControl
    {
        public ArmourFilter(object parent)
        {
            InitializeComponent();
            DataContext = new Controllers.FilterTabs.ArmourFilterControl(parent);
        }
    }
}
