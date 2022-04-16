using System;

namespace ES_HomeCare_API.Model
{
    public class BaseModel
    {
        public int EntityId { get; set; }
        public int UserId { get; set; }
        public short IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
