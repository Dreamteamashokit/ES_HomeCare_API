using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class MasterService: IMasterService
    {
        private readonly IMasterData data;
        public MasterService(IMasterData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<string>> CreateDiagnosis(DiagnosisItem _model)
        {
            return await data.CreateDiagnosis(_model);

        }

        public async Task<ServiceResponse<IEnumerable<DiagnosisItem>>> GetDiagnosis()

        {
            return await data.GetDiagnosis();

        }


    }
}
