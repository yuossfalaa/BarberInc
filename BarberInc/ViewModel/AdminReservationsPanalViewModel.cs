using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using Services.DomainServices.UserServices;

namespace BarberInc.ViewModel
{
    public  partial class AdminReservationsPanalViewModel:BaseViewModel
    {
        private readonly IReservationService _reservationService;
        bool Loaded = false;
        [ObservableProperty]
        DateTime from;
        [ObservableProperty]
        DateTime to;
        [ObservableProperty]
        ObservableCollection<Reservation> reservations;
        [ObservableProperty]
        string searchterm;

        public AdminReservationsPanalViewModel(IReservationService reservationService)
        {
            _reservationService = reservationService;
            To = DateTime.Now.Date;
            From = DateTime.Now.Date;
            Loaded = true;
            GetRecords();
        }

        [RelayCommand]
        public async Task GetRecords()
        {

            await Task.Run(async () =>
            {
                var records = await _reservationService.GetAllBySearch(Searchterm, From, To);
                Reservations = new ObservableCollection<Reservation>(records);
            });
        }

        partial void OnFromChanged(DateTime oldValue, DateTime newValue)
        {
            if (!Loaded) return;
            if (newValue.Date > To.Date)
            {
                From = oldValue;
            }
            GetRecords();

        }

        partial void OnToChanged(DateTime oldValue, DateTime newValue)
        {
            if (!Loaded) return;
            if (newValue.Date < From.Date)
            {
                To = oldValue;
            }
            GetRecords();
        }


    }
}
