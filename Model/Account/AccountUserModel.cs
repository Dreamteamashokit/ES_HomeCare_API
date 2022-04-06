namespace ES_HomeCare_API.Model.Account
{
    public class AccountUserModel: BaseModel
    {
        public int UserId { get; set; }
        public string UserKey { get; set; }

        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public string SSN { get; set; }
        public short UserType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; } 
        public short Gender { get; set; }
        public short Ethnicity { get; set; }
        public short MaritalStatus { get; set; }

        public int? HRSupervisor { get; set; }

        

    }
}
