using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberInc.States.Navigators
{
    public class Navigator : INavigator
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                StateChanged?.Invoke();

            }
        }
        public event Action StateChanged;

    }
}
