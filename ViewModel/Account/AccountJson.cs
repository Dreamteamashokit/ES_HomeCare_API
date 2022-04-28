namespace ES_HomeCare_API.ViewModel.Account
{
    public class AccountJson: BaseJson
    {
        public string UserKey { get; set; }
        public short UserType { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string EmgPhone { get; set; }
        public string EmgContact { get; set; }


        public short Gender { get; set; }
        public string GenderName { get; set; }
        public short MaritalStatus { get; set; }
        public string MaritalStatusName { get; set; }
        public short Ethnicity { get; set; }
        public string EthnicityName { get; set; }
        public int SupervisorId { get; set; }
        public string Supervisor { get; set; }
    }
}
