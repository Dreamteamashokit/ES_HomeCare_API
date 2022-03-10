using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class IncidentMode : BaseModel
    {
        public int IncidentId { get; set; }
        public int EmpId { get; set; }
        public int ClientId { get; set; }
        public string IncidentDate { get; set; }
        public string IncidentDetail { get; set; }

      
    }
}
