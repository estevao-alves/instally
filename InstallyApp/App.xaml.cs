using InstallyApp.Resources.Winget;

namespace InstallyApp
{
    public partial class App : System.Windows.Application
    {
        public static class Master
        {
            public static DebugStatus Debug;
            public static WingetData Winget;
            public static MainWindow Main;
        }

        private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            Master.Winget = new();

            Master.Main = new();
            Master.Main.Show();
        }


    }
}
