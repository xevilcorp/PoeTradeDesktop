using PoeTradeDesktop.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PoeTradeDesktop.UI.Components
{
    public partial class SearchPage : UserControl
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void SearchBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (SearchTextList.SelectedIndex == -1) SearchTextList.SelectedIndex = 0;

            switch (e.Key)
            {
                case Key.Down:
                    if (SearchTextList.ItemsSource != null)
                    {
                        int nItems = (SearchTextList.ItemsSource as List<SearchItem>).Count;
                        if (SearchTextList.SelectedIndex < nItems - 1)
                        {
                            SearchTextList.SelectedIndex++;
                            SearchTextList.ScrollIntoView(SearchTextList.SelectedItem);
                        }
                    }
                    break;
                case Key.Up:
                    if (SearchTextList.SelectedIndex > 0)
                    {
                        SearchTextList.SelectedIndex--;
                        SearchTextList.ScrollIntoView(SearchTextList.SelectedItem);
                    }
                    break;
                case Key.Enter:
                    Keyboard.ClearFocus();
                    break;
            }
        }

        SearchItem clickedItem = null;
        private void SearchBoxListClicked(object sender, MouseButtonEventArgs e)
        {
            clickedItem = (((StackPanel)sender).DataContext) as SearchItem;
        }

        private void SearchBoxLostFocus(object sender, RoutedEventArgs e)
        {
            SearchTextListPopup.StaysOpen = false;
            SearchTextListPopup.IsOpen = false;


            if (clickedItem != null)
            {
                SearchTextList.SelectedItem = clickedItem;
                clickedItem = null;
            }

            if (searchBox.Text.Trim() != "")
            {
                string newText = (DataContext as Controllers.SearchControl).SelectedSearchTextResult.Text;
                if (newText != "None")
                {
                    searchBox.Text = newText;
                }
            }

        }

        private void SearchBoxGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextListPopup.StaysOpen = true;
            SearchTextListPopup.IsOpen = true;
        }




        private void lv_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)e.OriginalSource;
            if (sv.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Visible) 
            {
                if (sv.ScrollableHeight - 100 <= sv.VerticalOffset)
                {
                    if(!(DataContext as Controllers.SearchControl).IsSearchResultLoading)
                    {
                        (DataContext as Controllers.SearchControl).IsSearchResultLoading = true;
                    }
                }
            }
        }

       
    }
}
