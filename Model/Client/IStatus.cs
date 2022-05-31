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
        public int StatusId { get; set; }
        public string ActivityText { get; set; }
        public DateTime StatusDate { get; set; }
        public string ReferralCodeText { get; set; }
        public string note { get; set; }
    }







    public class ClientStatusModel 
    {
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int ReferralCode { get; set; }
        public string StatusDate { get; set; }
        public string Note { get; set; }
        public short IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


    }







}
