namespace ES_HomeCare_API.ViewModel
{
    public class BaseJson
    {
        public int UserId { get; set; }
        public short IsActiveId { get; set; }
        public string IsActive { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }

    }
}
