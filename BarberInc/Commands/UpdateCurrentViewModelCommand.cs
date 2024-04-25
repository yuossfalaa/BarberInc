global using BarberInc.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarberInc.Factories;
using BarberInc.States.Navigators;
using static BarberInc.States.Navigators.INavigator;

namespace BarberInc.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(IViewModelFactory viewModelFactory, INavigator navigator)
        {
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                BaseViewModel viewModelBase = _viewModelFactory.CreateViewModel(viewType);
                if (viewModelBase != null)
                {
                    _navigator.CurrentViewModel = viewModelBase;
                }
            }
        }
    }
}
