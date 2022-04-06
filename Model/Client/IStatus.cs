using System;
using System.Collections.Generic;

namespace ES_HomeCare_API.Model.Client
{
    public class ClientStatus
    {
        public int ActivityId { get; set; }
        public DateTime Date { get; set; }
        public int ReferralCode { get; set; }
        public string Note { get; set; }
        public int OfficeUserId { get; set; }
        public int OfficeUserReferralID { get; set; }

        public int clientId{ get; set; }
        public bool text { get; set; }
        public bool screen { get; set; }
        public bool email { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ClientStatusLst
    {
        public string ActivityText { get; set; }
        public DateTime Date { get; set; }
        public string ReferralCodeText { get; set; }
        public string Note { get; set; }
       
    }


}
