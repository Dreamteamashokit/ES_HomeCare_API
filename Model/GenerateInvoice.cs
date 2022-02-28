using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class GenerateInvoice
    {
        public string description { get; set; }
        public string custId { get; set; }
        public string amount { get; set; }
    }
}
