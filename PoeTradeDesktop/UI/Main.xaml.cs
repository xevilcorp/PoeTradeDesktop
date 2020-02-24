using System.Windows;
using System.Windows.Input;

namespace PoeTradeDesktop.UI
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();

            if (e.ChangedButton == MouseButton.Left)
            {
                this.WindowState = WindowState.Normal;
                this.WindowStartupLocation = WindowStartupLocation.Manual;
                this.DragMove();
            }
        }

        private void Browser_Loaded(object sender, RoutedEventArgs e)
        {
            //browser.Source = new Uri("https://www.pathofexile.com/trade", UriKind.Absolute);
        }

        private void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            /*IHTMLDocument2 doc = (browser.Document) as IHTMLDocument2;
            IHTMLStyleSheet ss = doc.createStyleSheet("", 0);

            ss.cssText = @".logo,#statusBar, .bottom, .linkBack, .language-select, .navigation { display: none !important; } body {background: none !important; background-color: #000 !important; overflow-x: hidden; overflow-y: visible} #trade {margin-left: -10px !important; width: 390px} ";
            */
        }
    }
}
