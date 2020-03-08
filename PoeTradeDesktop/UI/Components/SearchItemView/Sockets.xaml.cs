using PoeTradeDesktop.Schemes.Searching._SearchResultItem._Item;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PoeTradeDesktop.UI.Components.SearchItemView
{
    /// <summary>
    /// The Socket Control that appears in front of the items that have sockets.
    /// </summary>
    public partial class Sockets : UserControl
    {
        public Sockets()
        {
            Loaded += SocketsLoaded;
        }

        /// <summary>
        /// All sockets and links by default have their visibility collapsed by design.
        /// For each socket in the socket list named Source.
        /// Keeping a count variable for each iteration.
        /// Starting from the number 1, based on the count, I turn on the visibility of the sockets.
        /// Based on the socket sColour property I give the socket an appropriate Image Source.
        /// 
        /// If the count goes higher than 1 the sockets need or a space or a link in between them.
        /// For space I change the visibility of the collapsed link to hidden.
        /// I change it based on count-1, so for count 2, link 1 space is activated, for count 6, link 5.
        /// 
        /// After every iteration I store the lastGroup of the socket, on the next iteration, 
        /// if the current group matches the previous group it means the sockets are connected, 
        /// when this happens, I change the visibility of the link in between them to visible instead of just hidden.
        /// </summary>
        private void SocketsLoaded(object sender, RoutedEventArgs e)
        {
            if (Source != null )
            {
                InitializeComponent();

                int count = 1;
                int lastGroup = -1;
                foreach (Socket s in Source)
                {
                    Image socket = GetSocket(count);
                    socket.Visibility = Visibility.Visible;

                    BitmapImage img = new BitmapImage(new Uri($"../../Images/socket_{s.SColour}.png", UriKind.Relative));
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    socket.Source = img;

                    if (count > 1)
                    {
                        Image link = GetLink(count - 1);
                        link.Visibility = Visibility.Hidden;
                        if (lastGroup == s.Group)
                        {
                            link.Visibility = Visibility.Visible;
                        }
                    }

                    lastGroup = s.Group;
                    count++;
                }

            }
        }

        /// <summary>
        /// Converts a number from 1-5 to a Link Image Control 1-5
        /// </summary>
        private Image GetLink(int l)
        {
            switch(l)
            {
                case 1: return this.link1;
                case 2: return this.link2;
                case 3: return this.link3;
                case 4: return this.link4;
                case 5: return this.link5;
            }

            return null;
        }

        /// <summary>
        /// Converts a socket from 1-6 to a Socket Image Control 1-6
        /// </summary>
        private Image GetSocket(int s)
        {
            switch (s)
            {
                case 1: return this.gem1;
                case 2: return this.gem2;
                case 3: return this.gem3;
                case 4: return this.gem4;
                case 5: return this.gem5;
                case 6: return this.gem6;
            }
            return null;
        }

        public List<Socket> Source
        {
            get { return (List<Socket>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(List<Socket>), typeof(Sockets));

    }
}
