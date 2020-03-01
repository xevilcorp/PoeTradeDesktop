using System;
using System.IO;
using System.Net;
using System.Windows;

namespace PoeTradeDesktop
{
    public partial class App : Application
    {
        public App()
        {
            FileInfo file = new FileInfo(Path.Combine(Environment.CurrentDirectory, $@"ImageCache\"));
            file.Directory.Create();

            UpdateData();
        }

        private void UpdateData()
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, $@"Data\");
            FileInfo file = new FileInfo(folderPath);
            file.Directory.Create();

            string url, path; 
            WebClient client = new WebClient();

            url = "https://raw.githubusercontent.com/xEvilCorp/PoeTradeDesktop/master/PoeTradeDesktop/Data/item-category.json";
            path = Path.Combine(folderPath, "item-category.json");
            client.DownloadFile(url, path);

            url = "https://raw.githubusercontent.com/xEvilCorp/PoeTradeDesktop/master/PoeTradeDesktop/Data/item-rarity.json";
            path = Path.Combine(folderPath, "item-rarity.json");
            client.DownloadFile(url, path);

            url = "https://raw.githubusercontent.com/xEvilCorp/PoeTradeDesktop/master/PoeTradeDesktop/Data/map-series.json";
            path = Path.Combine(folderPath, "map-series.json");
            client.DownloadFile(url, path);
        }
    }
}
