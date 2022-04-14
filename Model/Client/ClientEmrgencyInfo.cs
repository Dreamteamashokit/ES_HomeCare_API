using System;
using System.Collections.Generic;

namespace ES_HomeCare_API.Model.Client
{
    
   

    public class ClientEmrgencyInfo
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string NPINumber { get; set; }
        public string Address { get; set; }
        public string Relationship { get; set; }
        public string City { get; set; }
        public string License { get; set; }
        public DateTime LicenseExpires { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int UserId { get; set; }
        public bool Edit { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

    }
}
