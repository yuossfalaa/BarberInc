using Domain;
using Microsoft.AspNet.Identity;
using Services.DomainServices.UserServices.Exceptions;
using static Services.DomainServices.UserServices.IAuthenticationService;

namespace Services.DomainServices.UserServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHasher _passwordhasher;
        private readonly IUserService _userService;

        public AuthenticationService(IPasswordHasher passwordhasher, IUserService userService)
        {
            _passwordhasher = passwordhasher;
            _userService = userService;
        }

        public async Task<ChangePasswordResult> ChangePassword(User user, string NewPassword)
        {
            User UpdatedUser = user;
            string HashedPassword = _passwordhasher.HashPassword(NewPassword);
            UpdatedUser.PasswordHash = HashedPassword;
            await _userService.Update(UpdatedUser.Id, UpdatedUser);
            return ChangePasswordResult.Success;
        }

        public async Task<User> LogIn(string Email, string Password)
        {
            User StoredUser = null;
            if (!string.IsNullOrEmpty(Email))
            {
                StoredUser = await _userService.GetByEmail(Email);
            }

            if (StoredUser == null)
            {
                throw new UserNotFoundException(Email);

            }
            if (StoredUser.IsDeleted)
            {
                throw new UserNotFoundException(Email);
            }
            if (Password == null)
            {
                throw new WrongPasswordException();
            }
            PasswordVerificationResult passwordResult = _passwordhasher.VerifyHashedPassword(StoredUser.PasswordHash, Password);
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new WrongPasswordException();
            }
            if(StoredUser.Blocked) { throw new UserBlockedException(); }

            return StoredUser;
        }

        public async Task<RegistrationResult> Registre(User RegisterUser)
        {
            RegistrationResult result = RegistrationResult.Success;
            string HashedPassword = _passwordhasher.HashPassword(RegisterUser.PasswordHash);
            User EmailCheck = await _userService.GetByEmail(RegisterUser.Email);
            if (EmailCheck != null && !EmailCheck.IsDeleted)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }
            if (result == RegistrationResult.Success)
            {
                RegisterUser.PasswordHash = HashedPassword;
                await _userService.Create(RegisterUser);
            }
            return result;

        }
    }
}
