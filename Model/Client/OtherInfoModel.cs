using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Client
{


    public class OtherInfoModel : BaseModel
    {


        public int OtherId { get; set; }
        public string CASA3 { get; set; }
        public string ContactId { get; set; }
        public string InsuranceGrp { get; set; }
        public bool IsMedications { get; set; }
        public bool IsDialysis { get; set; }
        public bool IsOxygen { get; set; }
        public bool IsAids { get; set; }
        public bool IsCourtOrdered { get; set; }
        public string FlowRate { get; set; }
        public string ReunionLocations { get; set; }
        public string ShelterName { get; set; }
        public string TalCode { get; set; }
        public string Shelter { get; set; }
        public string Facility { get; set; }
        public string Room { get; set; }
        public string ServiceRequestDate { get; set; }
        public string CareDate { get; set; }
        public string DischargeDate { get; set; }
        public string Notes { get; set; }
        public string Allergies { get; set; }

    }
}
