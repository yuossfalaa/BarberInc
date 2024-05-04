using Domain;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.DomainServices.Common;

namespace Services.DomainServices.UserServices
{
    public class ReservationDataService : IReservationService
    {
        private readonly DBContextFactory _contextFactory;
        private readonly NonQueryDataService<Reservation> _nonQueryDataService;
        public ReservationDataService(DBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Reservation>(contextFactory);
        }
        public async Task<Reservation> Create(Reservation entity)
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                if (entity.User != null) entity.User = null;


                EntityEntry<Reservation> createdResult = await context.Set<Reservation>().AddAsync(entity);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return createdResult.Entity;

            }
        }

        public async Task<bool> Delete(Reservation entity)
        {
            return await _nonQueryDataService.Delete(entity);
        }

        public async Task<Reservation> Get(int id)
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                Reservation entity = new Reservation();
                try
                {
                    entity = await context.Set<Reservation>().Include(a => a.User)
                        .FirstOrDefaultAsync((e) => e.Id == id);

                }
                catch
                {

                }
                return entity;
            }
        }

        public async Task<List<Reservation>> GetAll()
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                List<Reservation> entities = new List<Reservation>();
                try
                {
                    entities = await context.Set<Reservation>()
                        .Include(a => a.User)
                        .ToListAsync();
                    return entities;
                }
                catch
                {

                }
                return entities;

            }
        }

        public async Task<Reservation> Update(int id, Reservation entity)
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                if (entity.User != null) entity.User = null;

                entity.Id = id;

                context.Set<Reservation>().Update(entity);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string message = ex.ToString();
                }


                return entity;
            }
        }
    }
}
