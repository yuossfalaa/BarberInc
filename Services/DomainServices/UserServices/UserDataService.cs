using Domain;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.DomainServices.Common;

namespace Services.DomainServices.UserServices;

public class UserDataService : IUserService
{
    private readonly DBContextFactory _contextFactory;
    private readonly NonQueryDataService<User> _nonQueryDataService;
    public UserDataService(DBContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _nonQueryDataService = new NonQueryDataService<User>(contextFactory);
    }
    public async Task<User> Create(User entity)
    {
        return await _nonQueryDataService.Create(entity);
    }

    public async Task<bool> Delete(User entity)
    {
        return await _nonQueryDataService.Delete(entity);
    }

    public async Task<User> Get(int id)
    {
        using (DBContext context = _contextFactory.CreateDbContext())
        {
            User entity = new User();
            try
            {
                entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Id == id);

            }
            catch
            {

            }
            return entity;
        }
    }

    public async Task<List<User>> GetAll()
    {
        using (DBContext context = _contextFactory.CreateDbContext())
        {
            List<User> entities = new List<User>();
            try
            {
                entities = await context.Set<User>().ToListAsync();
                return entities;
             }
            catch
            {

            }
            return entities;

        }
    }

    public async Task<User> GetByEmail(string Email)
    {
        using (DBContext context = _contextFactory.CreateDbContext())
        {
            User entity = new User();
            try
            {
                entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Email == Email &&  e.IsDeleted == false);

            }
            catch
            {

            }
            return entity;

        }
    }

    public async Task<User> Update(int id, User entity)
    {
        return await _nonQueryDataService.Update(id, entity);
    }
}
