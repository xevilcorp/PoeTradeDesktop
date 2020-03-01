using PoeTradeDesktop.Controllers;
using PoeTradeDesktop.UI.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PoeTradeDesktop.UI.Components.Generic
{
    public partial class NumberBox : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetCurrentValue(TextProperty, value);}
        }

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetCurrentValue(NumberProperty, value);}
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetCurrentValue(PlaceholderProperty, value);}
        }

        public NumberBox()
        {
            InitializeComponent();
        }

        private static void OnTextChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberBox n = sender as NumberBox;
            if (n != null) { n.OnTextChanged(); }
        }

        private static void OnNumberChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberBox n = sender as NumberBox;
            if (n != null) { n.OnNumberChanged(); }
        }

        private bool changedText = false;
        private bool changedNumber = false;

        protected virtual void OnTextChanged()
        {

            bool isNumeric = int.TryParse(Text, out _);
            if((!isNumeric && Text != ""))
            {
                Text = "";
                return;
            }

            if (!changedNumber)
            {
                changedText = true;
                if (Text != "")
                {
                    Number = Convert.ToInt32(Text);
                }
                else
                {
                    Number = 0;
                }
            }
            else
            {
                changedNumber = false;
            }

            if(Text != "")
            {
                txtPh.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtPh.Visibility = Visibility.Visible;
            }
        }

        protected virtual void OnNumberChanged()
        {

            if(!changedText)
            {
                changedNumber = true;
                if (Number != 0)
                {
                    Text = Number.ToString();
                }
                else
                {
                    Text = "";
                }
            }
            else
            {
                changedText = false;
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(NumberBox), new FrameworkPropertyMetadata(OnTextChangedCallBack));
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(NumberBox), new FrameworkPropertyMetadata(OnNumberChangedCallBack));
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(NumberBox));

        private void textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                && e.Key != Key.Back && e.Key != Key.Left && e.Key != Key.Right && e.Key != Key.LeftCtrl
                && e.Key != Key.RightCtrl && e.Key != Key.C && e.Key != Key.X && e.Key != Key.V)
            {
                e.Handled = true;
            }
        }

        private void textbox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(e.Delta > 1)
            {
                Number++;
            }
            else
            {
                if(Number != 0)
                {
                    Number--;
                }
            }
        }
    }


}
