using System;
using System.Collections.Generic;

namespace ES_HomeCare_API.Model.Client
{
    public class ClientStatus:BaseModel
    {
        public int ActivityId { get; set; }
        public string Date { get; set; }
        public int ReferralCode { get; set; }
        public string Note { get; set; }
        public int OfficeUserId { get; set; }
        public int OfficeUserReferralID { get; set; }
        public int ClientId{ get; set; }
        public bool Text { get; set; }
        public bool Screen { get; set; }
        public bool Email { get; set; }
      
    }

    public class ClientStatusLst
    {
        public string ActivityText { get; set; }
        public DateTime Date { get; set; }
        public string ReferralCodeText { get; set; }
        public string Note { get; set; }
       
    }


}
