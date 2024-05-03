using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class Reservation:DomainObject
    {
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public ServiceType Service { get; set; } = ServiceType.Haircut;
        public int UserId {  get; set; }
        
    }
}
