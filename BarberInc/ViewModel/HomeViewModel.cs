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
        private readonly IRenavigator LoginRenave;
        private readonly IRenavigator ReservationRenave;

        public ICommand ReservationRenavigatCommand { get; set; }

        public HomeViewModel(ViewModelDelegateRenavigator<LoginViewModel> loginRenave, ViewModelDelegateRenavigator<ReservationViewModel> reservationRenave)
        {
            LoginRenave = loginRenave;
            ReservationRenave = reservationRenave;
            ReservationRenavigatCommand = new RenavigateCommand(ReservationRenave);
        }
        [RelayCommand]
        void Logout ()
        {
            RenavigateCommand renavigateCommand = new RenavigateCommand(LoginRenave);
            renavigateCommand.Execute(null);
        }


    }
}
