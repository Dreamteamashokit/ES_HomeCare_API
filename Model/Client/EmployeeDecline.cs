using System;

namespace ES_HomeCare_API.Model.Client
{



    public class EmployeeDeclineJson: BaseJson
    {
        public int DeclinedId { get; set; }
        public string ReportedDate { get; set; }
        public int EmpId { get; set; }
        public short CaseType { get; set; }
        public string Reason { get; set; }
        public string StartDate { get; set; }
        public string Notes { get; set; }
    }

    public class BaseJson
    {

        public int UserId { get; set; }
        public short IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }







    public class EmployeeDecline :BaseModel
    {

        public int  DeclinedId { get; set; }
        public DateTime ReportedDate { get; set; }
        public int EmpId { get; set; }
        public short CaseType { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public string Notes { get; set; }

    }


    public class EmployeeDeclineView 
    {

        public int DeclinedId { get; set; }
        public DateTime ReportedDate { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public short CaseType { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public string Notes { get; set; }

    }






}
