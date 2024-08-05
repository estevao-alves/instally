using Instally.App.Models.ViewModels;

namespace Instally.App.Pages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ManageUsersPage : ContentPage
{
    public ManageUsersPage()
    {
        InitializeComponent();

        BindingContext = new ManageUserViewModel();
    }
}