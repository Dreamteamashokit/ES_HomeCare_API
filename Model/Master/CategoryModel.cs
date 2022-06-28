using System;

namespace ES_HomeCare_API.Model.Master
{
    public class CategoryModel : BaseModel
    {
        public int CategoryId { get; set; }
        public short UserTypeId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }

        public bool IsRecurring { get; set; }
        public RecurrTypeEnum RecurrType { get; set; }
        public int RecurrValue { get; set; }
        public RecurrSrcDateEnum RecurrSrcType { get; set; }
        public int RecurrNotifyDays { get; set; }
        public DateTime RecurrDate { get; set; }


    }
}
