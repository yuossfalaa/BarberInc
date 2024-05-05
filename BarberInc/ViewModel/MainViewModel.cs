using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
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

        private DispatcherTimer ChangeCheckerTimer;

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

            ChangeCheckerTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            ChangeCheckerTimer.Tick += CheckTheInternet;
            ChangeCheckerTimer.Start();
        }

        private async void CheckTheInternet(object? sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        using (var stream = client.OpenRead("http://www.google.com"))
                        {
                        }
                    }
                }
                catch
                {
                    MyMessageQueue.Enqueue("There's no Internet Connection , any Changes you make won't get Saved");
                    ChangeCheckerTimer.Stop();
                    await Task.Delay(30000);
                    ChangeCheckerTimer.Start();
                }

            });
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
