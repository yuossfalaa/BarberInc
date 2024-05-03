using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.Commands;
using BarberInc.States.Navigators;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain;
using BarberInc.States.Authenticators;
using MaterialDesignThemes.Wpf;
using CommunityToolkit.Mvvm.Input;
using static Services.DomainServices.UserServices.IAuthenticationService;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BarberInc.ViewModel
{
    public partial class SignUpViewModel : BaseViewModel
    {
        private readonly IRenavigator LoginViewModelRenavigat;
        public ICommand LoginViewModelRenavigatCommand { get; set; }
        private readonly IAuthenticators _authenticators;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        [ObservableProperty]
        private User user;

        public SignUpViewModel(ViewModelDelegateRenavigator<LoginViewModel> LoginViewModelRenavigat, IAuthenticators authenticators, ISnackbarMessageQueue snackbarMessageQueue)
        {
            this.LoginViewModelRenavigat = LoginViewModelRenavigat;
            LoginViewModelRenavigatCommand = new RenavigateCommand(this.LoginViewModelRenavigat);
            User = new User();
            _authenticators = authenticators;
            _snackbarMessageQueue = snackbarMessageQueue;
        }
        [RelayCommand]
        private async Task Register()
        {
            await Task.Run(async () =>
            {
                if (User.FullName.IsNullOrEmpty())
                {
                    _snackbarMessageQueue.Enqueue("Please Add The Full Name");
                    return;
                }
                if (User.Email.IsNullOrEmpty())
                {
                    _snackbarMessageQueue.Enqueue("Please Add an Email");
                    return;
                }
                if (!ValidateEmail(User.Email))
                {
                    _snackbarMessageQueue.Enqueue("Email is not Valid");
                    return;
                }
                if (User.PhoneNumber.IsNullOrEmpty())
                {
                    _snackbarMessageQueue.Enqueue("Please Add a Phone Number");
                    return;
                }
                if (User.PasswordHash.IsNullOrEmpty() || User.PasswordHash.Count() < 8)
                {
                    _snackbarMessageQueue.Enqueue("Please Write a Password Thats 8 Letters or Longer");
                    return;
                }
                RegistrationResult registrationResult = await _authenticators.Register(User);

                if (registrationResult == RegistrationResult.Success)
                {
                    _snackbarMessageQueue.Enqueue("User Registered Successfully");
                    User = new User();

                }
                if (registrationResult == RegistrationResult.EmailAlreadyExists)
                {
                    _snackbarMessageQueue.Enqueue("Email already Exist");

                }
            });
        }
        public static bool ValidateEmail(string Email_Address)
        {
            string reg_pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            Regex regex = new Regex(reg_pattern);

            return regex.IsMatch(Email_Address);
        }
    }
}
