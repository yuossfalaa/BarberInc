using Domain;
using Services.DomainServices.UserServices;

namespace BarberInc.States.Authenticators
{
    public interface IAuthenticators
    {
        User CurrentUser { get; set; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        Task ChangePassword(User user, string NewPassword);
        Task Login(string UserName, string Password);
        void Logout();
        Task<IAuthenticationService.RegistrationResult> Register(User inRegistrationUser);
    }
}