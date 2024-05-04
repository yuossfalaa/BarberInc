using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarberInc.Commands;
using BarberInc.States.Navigators;
using CommunityToolkit.Mvvm.Input;

namespace BarberInc.ViewModel
{
    public partial class HomeViewModel:BaseViewModel
    {
        private readonly IRenavigator ReservationRenave;

        public ICommand ReservationRenavigatCommand { get; set; }

        public HomeViewModel( ViewModelDelegateRenavigator<ReservationViewModel> reservationRenave)
        {
            ReservationRenave = reservationRenave;
            ReservationRenavigatCommand = new RenavigateCommand(ReservationRenave);
        }
     


    }
}
