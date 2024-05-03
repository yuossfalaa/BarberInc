using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum AccountStates
    {
        Admin,
        User
    }
    public class User : DomainObject
    {
        private string _FullName = "";

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

 
        private string _PasswordHash = "";

        public string PasswordHash
        {
            get { return _PasswordHash; }
            set { _PasswordHash = value; OnPropertyChanged(nameof(PasswordHash)); }
        }


        private string _Email = "";

        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(nameof(Email)); }
        }


        private string _PhoneNumber = "";

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }


        private bool _Blocked = false;

        public bool Blocked
        {
            get { return _Blocked; }
            set { _Blocked = value; OnPropertyChanged(nameof(Blocked)); }
        }


        private AccountStates _AccountState = AccountStates.User;

        public AccountStates AccountState
        {
            get { return _AccountState; }
            set { _AccountState = value; OnPropertyChanged(nameof(AccountState)); }
        }


 
    }
}
