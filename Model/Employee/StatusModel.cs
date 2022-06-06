using System;

namespace ES_HomeCare_API.Model.Employee
{

    public class StatusModel : BaseModel
    {
        public string StatusType { get; set; }
        public string Reason { get; set; }
        public bool Resume { get; set; }
        public bool Rehire { get; set; }
        public string EffectiveDate { get; set; }
        public string ReturnDate { get; set; }
        public string Note { get; set; }
        public long Scheduling { get; set; }
        public long OfficeUserId { get; set; }
        public long EmployeeId { get; set; }
        public long TypeStatusId { get; set; }
        public bool Text { get; set; }
        public bool Screen { get; set; }
        public bool Email { get; set; }
        public DateTime EffectiveDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }

        
    }

    public class AvailabilityStatus
    {
        public string EffectiveDate { get; set; }
        public string ReturnDate { get; set; }
        public string Note { get; set; }
        public bool Resume { get; set; }
        public bool Rehire { get; set; }
        public long TypeId { get; set; }
        public string StatusType { get; set; }
    }

}
