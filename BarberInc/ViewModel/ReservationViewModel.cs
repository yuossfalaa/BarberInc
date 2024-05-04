using System.Windows.Input;
using BarberInc.Commands;
using BarberInc.States.Authenticators;
using BarberInc.States.Navigators;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.IdentityModel.Tokens;
using Services.DomainServices.UserServices;

namespace BarberInc.ViewModel
{
    public partial class ReservationViewModel : BaseViewModel
    {
        private readonly IRenavigator homeRenavigat;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly IReservationService _reservationService;
        private readonly IAuthenticators _authenticators;
        public ICommand HomeRenavigatCommand { get; set; }
        [ObservableProperty]
        private Reservation newReservation;


        [ObservableProperty]
        bool isHaircutChecked;
        [ObservableProperty]
        bool isHaircutShaveChecked;
        [ObservableProperty]
        bool isHaircutSeniorChecked;
        [ObservableProperty]
        bool isHaircutKidsChecked;
        [ObservableProperty]
        bool isLineupChecked;
        [ObservableProperty]
        DateTime date;
        public ReservationViewModel(ViewModelDelegateRenavigator<HomeViewModel> homeRenavigat, ISnackbarMessageQueue snackbarMessageQueue, IReservationService reservationService, IAuthenticators authenticators)
        {
            this.homeRenavigat = homeRenavigat;
            HomeRenavigatCommand = new RenavigateCommand(homeRenavigat);
            _snackbarMessageQueue = snackbarMessageQueue;
            NewReservation = new Reservation();
            IsHaircutChecked = true;
            Date = DateTime.Now;
            _reservationService = reservationService;
            _authenticators = authenticators;
        }
        partial void OnIsHaircutCheckedChanged(bool value)
        {
            if (!value) { CheckandForceSelection(); return; }
            NewReservation.Service = ServiceType.Haircut;
            IsHaircutShaveChecked = false;
            IsHaircutSeniorChecked = false;
            IsHaircutKidsChecked = false;
            IsLineupChecked = false;
        }
        partial void OnIsHaircutShaveCheckedChanged(bool value)
        {
            if (!value) { CheckandForceSelection(); return; }
            NewReservation.Service = ServiceType.HaircutShave;
            IsHaircutChecked = false;
            IsHaircutSeniorChecked = false;
            IsHaircutKidsChecked = false;
            IsLineupChecked = false;
        }
        partial void OnIsHaircutSeniorCheckedChanged(bool value)
        {
            if (!value) { CheckandForceSelection(); return; }
            NewReservation.Service = ServiceType.HaircutSenior;
            IsHaircutChecked = false;
            IsHaircutShaveChecked = false;
            IsHaircutKidsChecked = false;
            IsLineupChecked = false;
        }
        partial void OnIsHaircutKidsCheckedChanged(bool value)
        {
            if (!value) { CheckandForceSelection(); return; }
            NewReservation.Service = ServiceType.HaircutKids;
            IsHaircutChecked = false;
            IsHaircutShaveChecked = false;
            IsHaircutSeniorChecked = false;
            IsLineupChecked = false;
        }
        partial void OnIsLineupCheckedChanged(bool value)
        {
            if (!value) { CheckandForceSelection(); return; }

            NewReservation.Service = ServiceType.Lineup;
            IsHaircutChecked = false;
            IsHaircutShaveChecked = false;
            IsHaircutSeniorChecked = false;
            IsHaircutKidsChecked = false;
        }
        partial void OnDateChanged(DateTime oldValue, DateTime newValue)
        {
            if (newValue.Date < DateTime.Now.Date)
            {
                _snackbarMessageQueue.Enqueue("You Can't Select a Day in the past");
                Date = oldValue;
            }
        }
        [RelayCommand]
        private async Task Book()
        {
            await Task.Run(async () =>
            {
                NewReservation.Date = Date;
                if (NewReservation.FullName.IsNullOrEmpty())
                {
                    _snackbarMessageQueue.Enqueue("Please Enter The Name");
                    return;
                }
                if (NewReservation.PhoneNumber.IsNullOrEmpty())
                {
                    _snackbarMessageQueue.Enqueue("Please Enter The Phone Number");
                    return;
                }
                NewReservation.UserId = _authenticators.CurrentUser.Id;
                try
                {
                    await _reservationService.Create(NewReservation);
                    _snackbarMessageQueue.Enqueue("Booked !!");
                    Clear();
                }
                catch
                {
                    _snackbarMessageQueue.Enqueue("An Error Happened, Check your Internet connection and try again");
                }

            });
        }
        private void Clear()
        {
            NewReservation = new Reservation();
            IsHaircutChecked = true;
            Date = DateTime.Now;
        }
        private void CheckandForceSelection()
        {
            if (!IsHaircutChecked && !IsHaircutShaveChecked && !IsHaircutSeniorChecked && !IsHaircutKidsChecked && !IsLineupChecked)
            {
                IsHaircutChecked = true ;
            }
        }     
    }           
}               
