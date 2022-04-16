using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Common
{
    public class DiagnosisItem :BaseModel
    {
        public string DxCodes { get; set; }
        
        public string Description { get; set; }

      
    }
}
