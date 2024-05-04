using Domain;

namespace Services.DomainServices.UserServices
{
    public interface IUserService : IDataService<User>
    {
        Task<User> GetByEmail(string Email);
        Task<List<User>> Search(string term);

    }
}
