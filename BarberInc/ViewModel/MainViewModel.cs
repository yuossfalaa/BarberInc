using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarberInc.Commands;
using BarberInc.Factories;
using BarberInc.States.Authenticators;
using BarberInc.States.Navigators;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace BarberInc.ViewModel
{
    public partial class MainViewModel:BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IAuthenticators _authenticators;
        private readonly IRenavigator _loginrenavigate;
        public ISnackbarMessageQueue MyMessageQueue { get; set; }

        public ICommand UpdateCurrentViewModelCommand { get; }
        public BaseViewModel CurrentViewModel => _navigator.CurrentViewModel;


        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory, ISnackbarMessageQueue messageQueue, IAuthenticators authenticators, ViewModelDelegateRenavigator<LoginViewModel> loginrenavigate)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _navigator.StateChanged += Navigator_StateChanged;
            MyMessageQueue = messageQueue;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_viewModelFactory, _navigator);

            UpdateCurrentViewModelCommand.Execute(INavigator.ViewType.Login);
            _authenticators = authenticators;
            _loginrenavigate = loginrenavigate;
        }
        private async void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        [RelayCommand]
        private async Task Logout()
        {
            _authenticators.Logout();
            _loginrenavigate.Renavigate();
        }
    }
}
