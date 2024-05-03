using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.AspNet.Identity;
using Services.DomainServices.UserServices;
using static Services.DomainServices.UserServices.IAuthenticationService;
namespace BarberInc.States.Authenticators
{
    public class Authenticators : IAuthenticators
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;

        public Authenticators(IAuthenticationService authenticationService, ISnackbarMessageQueue snackbarMessageQueue)
        {
            _authenticationService = authenticationService;
            _snackbarMessageQueue = snackbarMessageQueue;
        }
        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                StateChanged?.Invoke();
            }
        }
        public bool IsLoggedIn => CurrentUser != null;

        public event Action StateChanged;
        public async Task Login(string UserName, string Password)
        {
            CurrentUser = await _authenticationService.LogIn(UserName, Password);
        }
        public void Logout()
        {
            CurrentUser = null;
        }
        public async Task ChangePassword(User user, string NewPassword)
        {
            await _authenticationService.ChangePassword(user, NewPassword);
            _snackbarMessageQueue.Enqueue("Password Changed");

        }
        public async Task<RegistrationResult> Register(User inRegistrationUser)
        {
            return await _authenticationService.Registre(inRegistrationUser);
        }

    }
}
