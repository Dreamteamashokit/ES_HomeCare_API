namespace ES_HomeCare_API.Model.Common
{
    public class UserView
    {
        public long Id { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string EmergPhone { get; set; }
        public string EmergContact { get; set; }
        public AddressView Address { get; set; }
    }
}
