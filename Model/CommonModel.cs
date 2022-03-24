using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class ItemList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
    }

    public class SelectList
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }

    public class ItemObj
    {
        public int ItemId { get; set; }
        public short MasterType { get; set; }
        public string ItemName { get; set; }
    }
}
