﻿using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.Model.Meeting;
using ES_HomeCare_API.ViewModel;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Service.IService;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService mtgSrv;
       private readonly ICommanService comSrv;

        public MeetingController(IMeetingService _mtgSrv, ICommanService _comSrv)
        {
            this.mtgSrv = _mtgSrv;
            this.comSrv = _comSrv;
        }

        [HttpPost("addMeeting")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewMeeting([FromBody] MeetingJson model)
        {
            MeetingModel obj = new MeetingModel() {
                MeetingDate = model.MeetingDate.ParseDate(),
                MeetingNote = model.MeetingNote,
                ClientId = model.ClientId,
                StartTime=model.StartTime.ParseTime(),
                EndTime=model.EndTime.ParseTime(),
                EmpList=model.EmpList,
                CreatedBy=1,
                CreatedOn=DateTime.Now


            };

            
            return Ok(await mtgSrv.AddMeeting(obj));
        }

    }
}
