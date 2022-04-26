using ES_HomeCare_API.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Client
{
    public class ClientModel : AccountUserModel
    {
        public int ClientId { get; set; }
        public string BillTo { get; set; }
        public string Contact { get; set; }
        public int NurseId { get; set; }
        public short OfChild { get; set; }
        public string AltId { get; set; }
        public string ID2 { get; set; }
        public string ID3 { get; set; }
        public string InsuranceID { get; set; }
        public string WorkerName { get; set; }
        public string WorkerContact { get; set; }
        public string ReferredBy { get; set; }
        public short PriorityCode { get; set; }
        public bool TimeSlip { get; set; }
        public bool IsHourly { get; set; }
        public string CoordinatorName { get; set; }
        public string NurseName { get; set; }
        public string GenderName { get; set; }
        public string EthnicityName { get; set; }
        public string MaritalStatusName { get; set; }

    }

    public class ClientContactLog : BaseModel
    {
        public int ContactlogId { get; set; }
        public int OfficeUserId { get; set; }
        public int EmpId { get; set; }
        public string Reason { get; set; }
        public DateTime CallDateTime { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string Issue { get; set; }
        public string ActionTaken { get; set; }
        public string Notes { get; set; }
        public Boolean IsFollowUp { get; set; }
        public Boolean IsSchedule { get; set; }
    }

    public class ClientNote : BaseModel
    {
        public int NotesId { get; set; }
        public int NotesTypeId { get; set; }
        public string Notes { get; set; }
        public int OfficeUserId { get; set; }
        public int EmpId { get; set; }
        public short NotifyTypeId { get; set; }
    }

    public class ClientCommunityMaster:BaseModel
    {
        public int CommunityId { get; set; }       
        public string CommunityName { get; set; }
        public string CommunityAddress { get; set; }
        public string CommunityFloor { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }

    public class ClientCompliance : BaseModel
    {
        public int ClientComplianceId { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int Category { get; set; }
        public DateTime? ScreenDate { get; set; }
        public int SubCategory { get; set; }
        public DateTime? SignedDate { get; set; }
        public DateTime? MDOrderFdate { get; set; }
        public DateTime? MDOrderEDate { get; set; }
        public short IsReceived { get; set; }
        public int AttachFile { get; set; }
        public int EmpId { get; set; }
        public int OfficeUserId { get; set; }
        public short IsNotifyViaText { get; set; }
        public short IsNotifyViaScreen { get; set; }
        public short IsNotifyViaEmail { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}
