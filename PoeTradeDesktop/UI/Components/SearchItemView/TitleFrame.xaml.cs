using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
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

        public Influences Influences
        {
            get { return (Influences)GetValue(InfluencesProperty); }
            set { SetValue(InfluencesProperty, value); }
        }

        public TitleFrame()
        {
            InitializeComponent();
            Loaded += TitleFrameLoaded;
        }

        private void TitleFrameLoaded(object sender, RoutedEventArgs e)
        {
            string firstInfluence = "";
            string secondInfluence = "";
            
            if(Influences != null)
            {
                PropertyInfo[] props = Influences.GetType().GetProperties();
                int count = 0;
                while (secondInfluence == "" && count < props.Length)
                {
                    PropertyInfo p = props[count];
                    if ((bool)p.GetValue(Influences))
                    {
                        if (firstInfluence == "") firstInfluence = p.Name.ToLower();
                        else secondInfluence = p.Name.ToLower();
                    }
                    count++;
                }


                if (firstInfluence != "")
                {
                    if (secondInfluence == "") secondInfluence = firstInfluence;

                    Grid g = (Grid)((Grid)lbl.Content).Children[0];

                    Image img1 = new Image();
                    BitmapImage bmp1 = new BitmapImage(new Uri($"../../Images/symbol_{firstInfluence}.png", UriKind.Relative));
                    bmp1.CacheOption = BitmapCacheOption.OnLoad;
                    img1.Source = bmp1;
                    img1.Height = 27;
                    img1.VerticalAlignment = VerticalAlignment.Center;
                    img1.HorizontalAlignment = HorizontalAlignment.Center;
                    g.Children.Add(img1);

                    Image img2 = new Image();
                    BitmapImage bmp2 = new BitmapImage(new Uri($"../../Images/symbol_{secondInfluence}.png", UriKind.Relative));
                    bmp2.CacheOption = BitmapCacheOption.OnLoad;
                    img2.Source = bmp2;
                    img2.Height = 27;
                    img2.VerticalAlignment = VerticalAlignment.Center;
                    img2.HorizontalAlignment = HorizontalAlignment.Center;
                    g.Children.Add(img2);

                    img2.SetValue(Grid.ColumnProperty, 2);
                }
            }
           
            
        }

        public static readonly DependencyProperty InfluencesProperty = DependencyProperty.Register("Influences", typeof(Influences), typeof(TitleFrame));
        public static readonly DependencyProperty ItemFrameTypeProperty = DependencyProperty.Register("ItemFrameType", typeof(int), typeof(TitleFrame));
        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register("ItemName", typeof(string), typeof(TitleFrame));
        public static readonly DependencyProperty ItemTypeLineProperty = DependencyProperty.Register("ItemTypeLine", typeof(string), typeof(TitleFrame));
    }
}
