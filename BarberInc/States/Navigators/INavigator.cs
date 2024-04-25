using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberInc.States.Navigators
{
    public interface INavigator
    {
        public enum ViewType
        {
            Login,
            SignUp,
            Home,
            Reservation
        }
        BaseViewModel CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
