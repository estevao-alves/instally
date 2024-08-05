using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Instally.App.Models.ViewModels
{
    [QueryProperty(nameof(User), "User")]
    public partial class ManageUserViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNew))]
        public User user = new();

        bool IsNew
        {
            get
            {
                if (User.Id == Guid.Empty) return true;
                return false;
            }
        }

        public ManageUserViewModel() { }

        [RelayCommand]
        async Task Save()
        {
            if (IsNew)
            {
                Debug.WriteLine("---> Add new Item");
                await App.DataService.AddUserAsync(User);
            }
            else
            {
                Debug.WriteLine("---> Update Item");
                await App.DataService.UpdateUserAsync(User);
            }

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task Delete()
        {
            await App.DataService.DeleteUserAsync(User.Id);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
