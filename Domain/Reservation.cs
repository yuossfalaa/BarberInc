using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Domain
{
    public enum ServiceType
    {
        Haircut,
        HaircutShave,
        HaircutSenior,
        HaircutKids,
        Lineup,
    }
    public class Reservation : DomainObject
    {

        private string _FullName = "";

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; OnPropertyChanged(nameof(FullName)); }
        }


        private string _PhoneNumber = "";

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }


        private DateTime _Date = DateTime.Now;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged(nameof(Date)); }
        }


        private ServiceType _Service = ServiceType.Haircut;

        public ServiceType Service
        {
            get { return _Service; }
            set { _Service = value; OnPropertyChanged(nameof(Service)); }
        }

        public int? UserId { get; set; }
        public User? User { get; set; }

    }
}
