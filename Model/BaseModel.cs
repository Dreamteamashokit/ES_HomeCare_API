using System;

namespace ES_HomeCare_API.Model
{
    public class BaseModel
    {
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public short IsActive { get; set; }

    }
}
