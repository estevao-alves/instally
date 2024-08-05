using Instally.App.Models;

namespace Instally.App.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        collectionView.ItemsSource = await App.DataService.GetAllUsersAsync();
    }

    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("---> Add button clicked!");

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(User), new User() }
        };

        await Shell.Current.GoToAsync(nameof(ManageUsersPage), navigationParameter);
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("---> Item changed clicked!");

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(User), e.CurrentSelection.FirstOrDefault() as User }
        };

        await Shell.Current.GoToAsync(nameof(ManageUsersPage), navigationParameter);
    }
}
