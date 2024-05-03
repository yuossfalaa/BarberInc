using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services.DomainServices.UserServices
{
    public interface IAuthenticationService
    {
        public enum RegistrationResult
        {
            Success,
            EmailAlreadyExists
        }
        public enum ChangePasswordResult
        {
            Success,
            WrongPassword
        }
        Task<RegistrationResult> Registre(User RegisterUser);

        Task<User> LogIn(string Email, string Password);
        Task<ChangePasswordResult> ChangePassword(User user, string NewPassword);

    }
}
