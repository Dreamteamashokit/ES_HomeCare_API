namespace ES_HomeCare_API.Model.Client
{
    public class ClientEmrgencyInfo
    {
        public Contract PrimaryContract { get; set; }
        public Contract EmergencyContact { get; set; }
        public Physician PhysicianContact { get; set; }
    }

    public class Contract
    {
        public string Name { get; set; }

        public string Relationship { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class Physician
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string NPINumber { get; set; }
        public string Address { get; set; }

        public string CityStateZip { get; set; }
        public string License { get; set; }
        public string LicenseExpires { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
