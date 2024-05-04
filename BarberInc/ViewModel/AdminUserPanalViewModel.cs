using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using MaterialDesignThemes.Wpf;
using Services.DomainServices.UserServices;

namespace BarberInc.ViewModel
{
    public partial class AdminUserPanalViewModel:BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        [ObservableProperty]
        ObservableCollection<User> users;
        [ObservableProperty]
        string searchTerm;

        public AdminUserPanalViewModel(IUserService userService, ISnackbarMessageQueue snackbarMessageQueue)
        {
            _userService = userService;
            _snackbarMessageQueue = snackbarMessageQueue;
            GetRecords();
        }
        [RelayCommand]
        async Task GetRecords()
        {
            await Task.Run(async() =>
            {
                  var records = await _userService.Search(SearchTerm);
                Users = new ObservableCollection<User>(records);
            });
        }
        [RelayCommand]
        async Task BlockUser(User selectedUser)
        {
            await Task.Run(async () =>
            {
                if (selectedUser is null) return;
                selectedUser.Blocked = !selectedUser.Blocked;
                await _userService.Update(selectedUser.Id,selectedUser);
                _snackbarMessageQueue.Enqueue("Done");
            });
        }
    }
}
