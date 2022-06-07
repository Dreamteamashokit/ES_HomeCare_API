namespace ES_HomeCare_API.Model.Master
{
    public class CategoryModel : BaseModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
