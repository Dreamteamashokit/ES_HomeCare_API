
using Dapper;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.Model.Meeting;
using ES_HomeCare_API.WebAPI.Data.IData;

using ES_HomeCare_API.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data
{
    public class MeetingData : IMeetingData
    {

        private IConfiguration configuration;
        public MeetingData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> AddMeeting(MeetingModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblMeeting (ClientId,MeetingDate,StartTime,EndTime,MeetingNote,CreatedOn,CreatedBy)" +
                        " values(@ClientId,@MeetingDate,@StartTime,@EndTime,@MeetingNote,@CreatedOn,@CreatedBy) select SCOPE_IDENTITY();";

                    _model.MeetingId = (int)(cnn.Query<int>(sqlQuery, _model).First());

                    int rowsAffected = cnn.Execute(@"INSERT Into tblEmpClientMeeting (MeetingId,EmpId) 
                    values(@MeetingId,@EmpId)",
                    _model.EmpList.Select(c => new { MeetingId = _model.MeetingId, EmpId = c }));

                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                sres.Message = ex.Message;
                return sres;
            }
            finally
            {

            }
            return sres;
        }




        public async Task<ServiceResponse<IEnumerable<EmpMeeting>>> GetEmpMeetingList(int empId)
        {
            ServiceResponse<IEnumerable<EmpMeeting>> obj = new ServiceResponse<IEnumerable<EmpMeeting>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select  IsNull(p.LastName,'')  +' '+ p.FirstName as EmpName,q.EmpId,r.*,IsNull(s.LastName,'')+' '+s.FirstName as ClientName " +
                    " from tblEmployee p inner join tblEmpClientMeeting q on p.EmpId=q.EmpId inner join tblMeeting r on q.MeetingId=r.MeetingId inner join tblClient s on " +
                    "r.ClientId=s.ClientId where q.EmpId=@EmpId";

                IEnumerable<EmpMeeting> cmeetings = (await connection.QueryAsync<EmpMeeting>(sql,
                       new { @EmpId = empId }));

                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }


        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public IEnumerable<ClMeeting> Meetings { get; set; }


        public async Task<ServiceResponse<IEnumerable<ClientMeeting>>> GetClientMeetingList()
        {
            ServiceResponse<IEnumerable<ClientMeeting>> obj = new ServiceResponse<IEnumerable<ClientMeeting>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select x.ClientId,x.FirstName,x.MiddleName,x.LastName,x.Contact,z.EmpId,p.FirstName +' ' + ISNULL(p.MiddleName,' ')+' ' + p.LastName " +
                    "as EmpName,y.MeetingId,y.MeetingDate,y.StartTime,y.EndTime from tblClient x Left Join tblMeeting y on x.ClientId=y.ClientId inner join" +
                    " tblEmpClientMeeting z on y.MeetingId=z.MeetingId inner join tblEmployee p on z.EmpId=p.EmpId ;";

                var result = (await connection.QueryAsync(sql));


                //Using Query Syntax
                var GroupByQS = from mom in result
                                group mom by new { mom.ClientId, mom.FirstName, mom.MiddleName, mom.LastName, mom.Contact, } into momGroup
                                orderby momGroup.Key descending
                                select new ClientMeeting
                                {
                                    ClientId = momGroup.Key.ClientId,
                                    FirstName = momGroup.Key.FirstName,
                                    MiddleName = momGroup.Key.MiddleName,
                                    LastName = momGroup.Key.LastName,
                                    Contact = momGroup.Key.Contact,
                                    Meetings = momGroup.Select(x => new ClMeeting
                                    {
                                        EmpId = x.EmpId,
                                        EmpName = x.EmpName,
                                        MeetingId = x.MeetingId,
                                        MeetingDate = x.MeetingDate,
                                        StartTime = ((TimeSpan)x.StartTime).TimeHelper(),
                                        EndTime = ((TimeSpan)x.EndTime).TimeHelper(),
                                    })
                                };


                obj.Data = GroupByQS;
                obj.Result = GroupByQS.Any() ? true : false;
                obj.Message = GroupByQS.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }












    }
}
