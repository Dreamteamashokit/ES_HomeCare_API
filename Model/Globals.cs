using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{

    public enum Status
    {
        [Description("Deleted")]
        InActive = 0,
        [Description("Active")]
        Active = 1,
        [Description("Suspended")]
        Suspended = 2,
       
    }


    public enum MeetingEnum
    {

        [Description("Deleted")]
        InActive = 0,
        [Description("Active")]
        Active = 1,
        [Description("Cancelled")]
        Cancelled = 2,
        [Description("Cancelled By Client")]
        CancelledByClient = 3,       

    }

    public enum SqlQueryType
    {

        [Description("Insert")]
        Create = 1,
        [Description("Update")]
        Modify = 2,
        [Description("Delete")]
        Delete = 3,
        [Description("select")]
        select = 4,

    }


    public enum EmergencyInfoType
    {

        [Display(Name = "Primary Contact")]
        Primary = 1,
        [Display(Name = "Emergency Contact")]
        EmergencyInfo = 2,
        [Display(Name = "Physician")]
        Physician = 3,
        [Display(Name = "other")]
        other = 4,

    }



    public enum UserType
    {
        [Display(Name = "Super Admin")]
        SuperAdmin = 1,
        [Display(Name = "Administrators")]
        Administrators = 2,     
        [Display(Name = "Coordinators")]
        Coordinators =3,
        [Display(Name = "Human Resources")]
        HR = 4,
        [Display(Name = "Nursing Supervisors")]
        Nursing = 5,
        [Display(Name = "Office Staff")]
        OfficeStaff = 6,
        [Display(Name = "Billing")]
        Billing = 7,
        [Display(Name = "Employee")]
        Employee = 8,
        [Display(Name = "Client")]
        Client = 9,
    }















}
