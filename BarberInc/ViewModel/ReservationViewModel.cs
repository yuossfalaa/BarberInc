using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarberInc.Commands;
using BarberInc.States.Navigators;

namespace BarberInc.ViewModel
{
    public class ReservationViewModel:BaseViewModel
    {
        private readonly IRenavigator homeRenavigat;
        public ICommand HomeRenavigatCommand { get; set; }

        public ReservationViewModel(ViewModelDelegateRenavigator<HomeViewModel> homeRenavigat)
        {
            this.homeRenavigat = homeRenavigat;
            HomeRenavigatCommand = new RenavigateCommand(homeRenavigat);
        }


    }
}
