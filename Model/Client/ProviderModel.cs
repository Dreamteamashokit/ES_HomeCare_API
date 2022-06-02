namespace ES_HomeCare_API.Model.Client
{
    public class ProviderModel : BaseModel
    {
        public short ContactType { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NPINumber { get; set; }
        public string Address { get; set; }
        public string Relationship { get; set; }
        public string City { get; set; }
        public string License { get; set; }
        public string LicenseExpires { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
