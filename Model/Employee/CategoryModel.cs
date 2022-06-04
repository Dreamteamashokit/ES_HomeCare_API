namespace ES_HomeCare_API.Model.Employee
{
    public class CategoryModel : BaseModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public int ParentCategoryName { get; set; }
    }
}
