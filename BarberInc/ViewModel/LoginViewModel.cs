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
    public class LoginViewModel:BaseViewModel
    {
        private readonly IRenavigator signupRenavigat;
        public ICommand SignUpRenavigatCommand { get; set; }

        public LoginViewModel(ViewModelDelegateRenavigator<SignUpViewModel> signupRenavigat)
        {
            this.signupRenavigat = signupRenavigat;
            SignUpRenavigatCommand = new RenavigateCommand(this.signupRenavigat);
        }
    }
}
