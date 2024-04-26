using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.Commands;
using BarberInc.States.Navigators;
using System.Windows.Input;

namespace BarberInc.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IRenavigator LoginViewModelRenavigat;
        public ICommand LoginViewModelRenavigatCommand { get; set; }

        public SignUpViewModel(ViewModelDelegateRenavigator<LoginViewModel> LoginViewModelRenavigat)
        {
            this.LoginViewModelRenavigat = LoginViewModelRenavigat;
            LoginViewModelRenavigatCommand = new RenavigateCommand(this.LoginViewModelRenavigat);
        }
    }
}
