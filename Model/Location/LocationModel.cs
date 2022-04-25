namespace ES_HomeCare_API.Model.Location
{
    public class LocationModel : BaseModel
    {
        public int LocationId { get; set; }
        public int CompanyId { get; set; }
        public string LocationName { get; set; }
        public string BillingName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsBilling { get; set; }
        public string Description { get; set; }
        public string TaxId { get; set; }
        public string LegacyId { get; set; }
        public string NPI { get; set; }
        public string ISA06 { get; set; }


    }
}
