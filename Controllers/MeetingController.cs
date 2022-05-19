using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
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
            try
            {
                MeetingModel obj = new MeetingModel()
                {
                    MeetingDate = model.MeetingDate.ParseDate(),
                    MeetingNote = model.MeetingNote,
                    ClientId = model.ClientId,
                    EmpId = model.EmpId,                    
                    StartTime = model.StartTime.ParseTime(),
                    EndTime = model.EndTime.ParseTime(),
                    EmpList = model.EmpList,
                    IsStatus= (short)MeetingEnum.Active,
                    CreatedBy = model.UserId,
                    CreatedOn = DateTime.Now
                };
                return Ok(await mtgSrv.AddMeeting(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpGet("getEmpMeeting/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpMeetingList(int empId)
        {
            return Ok(await mtgSrv.GetEmpMeetingList(empId));
        }





        [HttpGet("getUserMeeting/{userId}/{userTypeId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserMeetingList(int userId, short userTypeId)
        {
            return Ok(await mtgSrv.GetUserMeetingList(userId, userTypeId));
        }













        [HttpGet("getClientMeetingList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientMeetingList()
        {
            return Ok(await mtgSrv.GetClientMeetingList());
        }

        [HttpPost("getClientMeetingList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientMeetingList([FromBody] ClientFilter model)
        {
            return Ok(await mtgSrv.GetClientMeetingList(model));
        }




        [HttpGet("getMeetingDetail/{meetingId}")]
        [ProducesResponseType(typeof(ServiceResponse<MeetingView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<MeetingView>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingDetail(long meetingId)
        {
            return Ok(await mtgSrv.GetMeetingDetail(meetingId));
        }













        [HttpPost("updateMeeting")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMeeting([FromBody] MeetingJson model)
        {
            try
            {
                MeetingModel obj = new MeetingModel()
                {
                 
                    ClientId = model.ClientId,
                    StartTime = model.StartTime.ParseTime(),
                    EndTime = model.EndTime.ParseTime(),
                    CreatedBy = model.UserId,
                    CreatedOn = DateTime.Now
                };
                return Ok(await mtgSrv.AddMeeting(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("addNote")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNote([FromBody] NotesModel model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                return Ok(await mtgSrv.PostNote(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("updateStatus")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus([FromBody] MeetingStatus model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                return Ok(await mtgSrv.ChangeStatus(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
