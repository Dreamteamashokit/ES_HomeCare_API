namespace ES_HomeCare_API.Model.Master
{
    public class CategoryModel : BaseModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }

    }
}
