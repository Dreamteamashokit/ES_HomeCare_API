using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Location;
using ES_HomeCare_API.ViewModel;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
       
        private IConfiguration configuration;
        private ILocationService locSrv;
        public LocationController( IConfiguration _configuration, ILocationService _locSrv)
        {
            configuration = _configuration;

            locSrv = _locSrv;

        }

        [HttpPost("addLocation")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLocation([FromBody] LocationModel model)
        {
            try
            {
                model.IsActive = 1;       
                model.CreatedOn = DateTime.Now;

                model.ModifiedBy = model.CreatedBy;
                model.ModifiedOn = model.CreatedOn;

                return Ok(await locSrv.AddLocation(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpGet("getLocationList")]
        [ProducesResponseType(typeof(ServiceResponse<List<LocationModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<LocationModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLocationList()
        {
            return Ok(await locSrv.GetLocationList());
        }


        [HttpGet("searchLocation/{search}")]
        [ProducesResponseType(typeof(ServiceResponse<List<LocationModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<LocationModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchLocation(string search)
        {
            return Ok(await locSrv.SearchLocation(search.Trim()));
        }


        [HttpGet("searchAvailbility")]
        [ProducesResponseType(typeof(ServiceResponse<List<AvailbilityReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<AvailbilityReponse>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchAvailbility([FromQuery] AvailbilityRequestJson obj)
        {
            var model = new AvailbilityRequest() {
                
                CaseId= obj.CaseId,
                EmpTypeId = obj.EmpTypeId,
                PayTypeId=obj.PayTypeId,
                FromDate = obj.FromDate.ParseDate(),
                ToDate = obj.ToDate.ParseDate(),
                TimeIn = obj.TimeIn.ParseTime("hh:mm:ss"),
                TimeOut = obj.TimeOut.ParseTime("hh:mm:ss"),
                ProvisionsList=obj.ProvisionsList,

            };
            return Ok(await locSrv.SearchAvailbility(model));
        }


    }
}
