using ES_HomeCare_API.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface IMasterData
    {
        Task<ServiceResponse<string>> CreateDiagnosis(DiagnosisItem _model);
        Task<ServiceResponse<IEnumerable<DiagnosisItem>>> GetDiagnosis();
    }
}
