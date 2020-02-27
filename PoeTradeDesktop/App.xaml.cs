using System;
using System.IO;
using System.Windows;

namespace PoeTradeDesktop
{
    public partial class App : Application
    {
        public App()
        {
            FileInfo file = new FileInfo(Path.Combine(Environment.CurrentDirectory, $@"ImageCache\"));
            file.Directory.Create();
        }

        private void UpdateData()
        {
            FileInfo file = new FileInfo(Path.Combine(Environment.CurrentDirectory, $@"Data\"));

        }
    }
}
