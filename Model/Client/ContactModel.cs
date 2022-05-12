namespace ES_HomeCare_API.Model.Client
{
    public class ContactModel:BaseModel
    {
        public short ContactType { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
