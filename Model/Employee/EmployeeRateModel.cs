using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Employee
{
       public class EmployeeRateModel:BaseModel
    {
        public string EffectiveDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int Hourly { get; set; }
        public int Livein { get; set; }
        public int Visit { get; set; }
        public int OverHourly { get; set; }
        public int OverLivein { get; set; }
        public int OverVisit { get; set; }
        public bool ApplyRateCheck { get; set; }
        public int OptionalHour { get; set; }
        public int OptionalAddHour { get; set; }
        public int Mileage { get; set; }
        public int TravelTime { get; set; }
        public int Gas { get; set; }
        public int Extra { get; set; }
        public int PayerId { get; set; }
        public int ClientId { get; set; }
        public int EmpId { get; set; }
       
    }

    public class EmpRate
    {
        public bool Active { get; set; }
        public string EffectiveDates { get; set; }
        public string Ragularrate { get; set; }
        public string OverTimeRate { get; set; }
        public string Discription { get; set; }
    }
}
