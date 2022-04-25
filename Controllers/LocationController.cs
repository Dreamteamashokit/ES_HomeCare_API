﻿using ES_HomeCare_API.Model.Location;
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



    }
}
