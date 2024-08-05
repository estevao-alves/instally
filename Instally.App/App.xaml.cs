using Instally.App.DataServices;

namespace Instally.App
{
    public partial class App : Application
    {
        public static IRestDataService DataService;

        public App(IRestDataService dataService)
        {
            InitializeComponent();

            MainPage = new AppShell();

            DataService = dataService;
        }
    }
}
