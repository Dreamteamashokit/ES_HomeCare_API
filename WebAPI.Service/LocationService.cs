using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Location;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationData data;
        public LocationService(ILocationData ldata)
        {
            data = ldata;
        }
        public async Task<ServiceResponse<string>> AddLocation(LocationModel model)
        {
            return await data.AddLocation(model);
        }

        public async Task<ServiceResponse<IEnumerable<LocationModel>>> GetLocationList()
        {
            return await data.GetLocationList();
        }
        public async Task<ServiceResponse<IEnumerable<LocationModel>>> SearchLocation(string search)
        {
            return await data.SearchLocation(search);
        }


        public async Task<ServiceResponse<IEnumerable<AvailbilityReponse>>> SearchAvailbility(AvailbilityRequest req)
        {
            return await data.SearchAvailbility(req);
        }

        


    }
}
