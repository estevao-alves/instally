using System.Windows;
using WindowsGuide_WPF.Resources.Winget;

namespace WindowsGuide_WPF
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
