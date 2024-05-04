using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.States.Navigators;
using BarberInc.ViewModel;
using BarberInc.Views;

namespace BarberInc.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<SignUpViewModel> _createSignUpViewModel;
        private readonly CreateViewModel<ReservationViewModel> _createReservationViewModel;
        private readonly CreateViewModel<AdminDashViewModel> _createAdminDashViewModel;

        public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<SignUpViewModel> createSignUpViewModel, CreateViewModel<ReservationViewModel> createReservationViewModel,
            CreateViewModel<AdminDashViewModel> createAdminDashViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createSignUpViewModel = createSignUpViewModel;
            _createReservationViewModel = createReservationViewModel;
            _createAdminDashViewModel = createAdminDashViewModel;
        }

        public BaseViewModel CreateViewModel(INavigator.ViewType viewType)
        {
            switch (viewType)
            {
                case INavigator.ViewType.Login:
                    return _createLoginViewModel();

                    break;
                case INavigator.ViewType.SignUp:
                    return _createSignUpViewModel();

                    break;
                case INavigator.ViewType.Home:
                    return _createHomeViewModel();
                    break;
                case INavigator.ViewType.Reservation:
                    return _createReservationViewModel();
                    break;               
                case INavigator.ViewType.AdminDash:
                    return _createAdminDashViewModel();
                    break;
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}