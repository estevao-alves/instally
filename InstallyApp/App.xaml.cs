using System.Windows;
using InstallyApp.Components.Layout;
using InstallyApp.Components.Popups;
using InstallyApp.Resources.Winget;

namespace InstallyApp
{
    public partial class App : Application
    {
        public static class Master
        {
            public static MainWindow Main;
            public static WingetData Winget;
        }

        public App() {
            InitializeComponent();

            Master.Winget = new();
            Master.Main = new();
            Master.Main.Show();
        }

    }
}
