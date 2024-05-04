using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BarberInc.ViewModel
{
    public partial class AdminDashViewModel : BaseViewModel
    {
        [ObservableProperty]
        AdminUserPanalViewModel adminUserPanalViewModel;
        [ObservableProperty]
        AdminReservationsPanalViewModel adminReservationsPanalViewModel;

        public AdminDashViewModel(AdminReservationsPanalViewModel adminReservationsPanalViewModel, AdminUserPanalViewModel adminUserPanalViewModel)
        {
            AdminReservationsPanalViewModel = adminReservationsPanalViewModel;
            AdminUserPanalViewModel = adminUserPanalViewModel;
        }
    }
}
