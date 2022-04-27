﻿using ES_HomeCare_API.Model.Location;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface ILocationService
    {
        Task<ServiceResponse<string>> AddLocation(LocationModel _model);
        Task<ServiceResponse<IEnumerable<LocationModel>>> GetLocationList();
        Task<ServiceResponse<IEnumerable<LocationModel>>> SearchLocation(string search);
    }
}