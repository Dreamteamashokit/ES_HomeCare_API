using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Client
{
    public class DiagnosisModel : BaseModel
    {
        public int DxId { get; set; }
        public short OrderNo { get; set; }
        public bool IsPrimary { get; set; }


    }


    public class DiagnosisView
    {
        public int DxId { get; set; }
        public short OrderNo { get; set; }
        public bool IsPrimary { get; set; }

        public string DxCode { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

    }



  













}
