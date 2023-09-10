using System.Collections.Generic;
using InstallyApp.Application.Contexts;
using InstallyApp.Resources.Winget;

namespace InstallyApp
{
    public partial class App : System.Windows.Application
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
