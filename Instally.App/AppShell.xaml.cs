using Instally.App.Pages;

namespace Instally.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ManageUsersPage), typeof(ManageUsersPage));
        }
    }
}
