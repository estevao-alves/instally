using System.Windows;
using WindowsGuide_WPF.Resources.Winget;
using WindowsGuide_WPF.Components.Items;

namespace WindowsGuide_WPF
{
    public partial class App : Application
    {
        public static class Master
        {
            public static MainWindow Main;
            public static WingetData Winget;
            public static AppInSearchList Items;
        }

        public App() {
            InitializeComponent();

            Master.Winget = new();
            Master.Main = new();
            Master.Main.Show();
        }

    }
}
