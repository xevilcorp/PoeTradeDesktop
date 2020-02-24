using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PoeTradeDesktop.UI.Custom
{
    public class TextHighlightTextBlock : TextBlock
    {
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public TextHighlightTextBlock() : base()
        {
            Loaded += TextBlockLoaded;
        }

        private void TextBlockLoaded(object sender, RoutedEventArgs e)
        {
            if (Text.Length == 0)
                return;

            try
            {
                string textUpper = Text.ToUpper();
                string toFind = SearchText.ToUpper();
                int firstIndex = textUpper.IndexOf(toFind);
                string firstStr = Text.Substring(0, firstIndex);
                string foundStr = Text.Substring(firstIndex, toFind.Length);
                string endStr = Text.Substring(firstIndex + toFind.Length, Text.Length - (firstIndex + toFind.Length));

                Inlines.Clear();
                var run = new Run();
                run.Text = firstStr;
                Inlines.Add(run);
                run = new Run();
                run.FontWeight = FontWeights.Bold;
                run.Foreground = new SolidColorBrush(new Color { R = 187, G = 158, B = 116, A = 255 });
                run.Text = foundStr;
                Inlines.Add(run);
                run = new Run();
                run.Text = endStr;
                Inlines.Add(run);
            }
            catch (Exception) { }
        }

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register("SearchText", typeof(string), typeof(TextHighlightTextBlock));
    }
}