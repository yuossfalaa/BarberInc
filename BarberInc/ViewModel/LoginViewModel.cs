using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarberInc.Commands;
using BarberInc.Models;
using BarberInc.States.Authenticators;
using BarberInc.States.Navigators;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Services.DomainServices.UserServices.Exceptions;

namespace BarberInc.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IRenavigator signupRenavigat;
        private readonly IRenavigator homeRenavigat;
        private readonly IRenavigator AdminDashRenavigat;
        private readonly IAuthenticators _authenticators;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        public ICommand SignUpRenavigatCommand { get; set; }
        public ICommand HomeRenavigatCommand { get; set; }

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string password;


        public LoginViewModel(ViewModelDelegateRenavigator<SignUpViewModel> signupRenavigat, ViewModelDelegateRenavigator<HomeViewModel> homeRenavigat, IAuthenticators authenticators, 
            ISnackbarMessageQueue snackbarMessageQueue, 
            ViewModelDelegateRenavigator<AdminDashViewModel> adminDashRenavigat)
        {
            this.signupRenavigat = signupRenavigat;
            SignUpRenavigatCommand = new RenavigateCommand(this.signupRenavigat);
            this.homeRenavigat = homeRenavigat;
            HomeRenavigatCommand = new RenavigateCommand(this.homeRenavigat);
            _authenticators = authenticators;
            _snackbarMessageQueue = snackbarMessageQueue;
            Email = string.Empty;
            Password = string.Empty;
            AdminDashRenavigat = adminDashRenavigat;
        }
        [RelayCommand]
        public async Task Login()
        {
            await Task.Run(async () =>
            {
                App_Settings settings = new App_Settings();
                if (Email == settings.appSettings.UserName && Password == settings.appSettings.Password)
                {
                    AdminDashRenavigat.Renavigate();
                    return;
                }
                try
                {
                    await _authenticators.Login(Email, Password);
                    HomeRenavigatCommand.Execute(null);
                    Email = string.Empty;
                    Password = string.Empty;
                }
                catch (UserNotFoundException ex)
                {
                    _snackbarMessageQueue.Enqueue("No User Was Found");
                }
                catch (WrongPasswordException ex)
                {
                    _snackbarMessageQueue.Enqueue("Password or Email are Wrong");
                }
                catch (UserBlockedException ex)
                {
                    _snackbarMessageQueue.Enqueue("This User Was Blocked By an Admin");

                }
                catch (Exception ex)
                {
                    _snackbarMessageQueue.Enqueue(ex);
                }
                finally
                {

                }
            });
        }
    }
}
