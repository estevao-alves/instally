using System.Collections.Generic;
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
            public static List<string> AppsJaAdicionados;
        }

        public App() {
            InitializeComponent();

            Master.Winget = new();
            Master.AppsJaAdicionados = new();

            Master.Main = new();
            Master.Main.Show();
        }

    }
}
