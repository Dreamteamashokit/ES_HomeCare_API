using System;
using System.Collections.Generic;
using System.ComponentModel;
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
}
