using Domain;

namespace Services.DomainServices.UserServices
{
    public interface IReservationService : IDataService<Reservation>
    {
        Task<List<Reservation>> GetAllBySearch(string term,DateTime from,DateTime to);

    }
}
