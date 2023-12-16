namespace InstallyApp
{
    public partial class App : System.Windows.Application
    {
        public static class Master
        {
            public static DebugStatus Debug;
            public static MainWindow Main;
        }

        private async void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            await WingetData.CarregarPacotesDaAPI();

            Master.Main = new();
            Master.Main.Show();
        }
    }
}
