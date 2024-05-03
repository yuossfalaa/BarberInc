using Domain;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
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
            return await _nonQueryDataService.Create(entity);
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
                    entity = await context.Set<Reservation>().FirstOrDefaultAsync((e) => e.Id == id);

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
                    entities = await context.Set<Reservation>().ToListAsync();
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
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
