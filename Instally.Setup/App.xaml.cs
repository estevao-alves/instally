namespace Instally.Setup
{
    public static class Master
    {
        public static MainWindow Main;

        public static string InstallationStatus
        {
            get
            {
                if (Main is not null) return Main.MainText.Text;
                else return "";
            }
            set {
                if (Main is not null) Main.MainText.Text = value;
            }
        }
    }

    public partial class App : System.Windows.Application
    {
        public App() {}

        private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            Master.Main = new();
            Master.Main.Show();
            Master.Main.VerificarAplicativo();
        }
    }
}
