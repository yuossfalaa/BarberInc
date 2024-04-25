using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.States.Navigators;

namespace BarberInc.Factories
{

    public interface IViewModelFactory
    {
        BaseViewModel CreateViewModel(INavigator.ViewType viewType);
    }
}
