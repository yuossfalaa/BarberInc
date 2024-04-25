using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarberInc.Commands;
using BarberInc.Factories;
using BarberInc.States.Navigators;
using MaterialDesignThemes.Wpf;

namespace BarberInc.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        public ISnackbarMessageQueue MyMessageQueue { get; set; }

        public ICommand UpdateCurrentViewModelCommand { get; }
        public BaseViewModel CurrentViewModel => _navigator.CurrentViewModel;


        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_viewModelFactory, _navigator);
            UpdateCurrentViewModelCommand.Execute(INavigator.ViewType.Login);

        }
        private async void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
