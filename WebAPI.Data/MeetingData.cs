
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
using ES_HomeCare_API.Model.Common;

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
                    string sqlQuery = "Insert Into tblMeeting (ClientId,MeetingDate,StartTime,EndTime,IsStatus,CreatedOn,CreatedBy)" +
                        " values(@ClientId,@MeetingDate,@StartTime,@EndTime,@IsStatus,@CreatedOn,@CreatedBy) select SCOPE_IDENTITY();";
                    _model.MeetingId = (int)(cnn.Query<int>(sqlQuery, _model).First());
                    int rowsAffected = cnn.Execute(@"INSERT Into tblEmpClientMeeting (MeetingId,EmpId) 
                    values(@MeetingId,@EmpId)",
                    _model.EmpList.Select(c => new { MeetingId = _model.MeetingId, EmpId = c }));
                    if (rowsAffected > 0)
                    {
                        if (!string.IsNullOrEmpty(_model.MeetingNote))
                        {
                            string addComment = "INSERT INTO tblMeetingPoint (MeetingId,MeetingPoint,CreatedOn,CreatedBy) VALUES (@MeetingId,@MeetingPoint,@CreatedOn,@CreatedBy)";
                            var result = cnn.Execute(addComment, new { _model.MeetingId, MeetingPoint= _model.MeetingNote, _model.CreatedOn, _model.CreatedBy });
                            sres.Result = true;
                        }
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
                    "r.ClientId=s.ClientId where q.EmpId=@EmpId and r.IsStatus<>0";

                IEnumerable<EmpMeeting> cmeetings = (await connection.QueryAsync<EmpMeeting>(sql,
                       new { @EmpId = empId }));

                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<EmpMeeting>>> GetUserMeetingList(int _userId, short _userTypeId)
        {

            ServiceResponse<IEnumerable<EmpMeeting>> obj = new ServiceResponse<IEnumerable<EmpMeeting>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select  IsNull(p.LastName,'')  +' '+ p.FirstName as EmpName,q.EmpId,r.*,IsNull(s.LastName,'')+' '+s.FirstName as ClientName " +
                    "from tblEmployee p inner join tblEmpClientMeeting q on p.EmpId=q.EmpId inner join tblMeeting r on q.MeetingId=r.MeetingId " +
                    "inner join tblClient s on r.ClientId=s.ClientId where r.IsStatus<>0 and r.ClientId=@UserId ";

                IEnumerable<EmpMeeting> cmeetings = (await connection.QueryAsync<EmpMeeting>(sql,
                       new { UserId = _userId }));

                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<ClientMeeting>>> GetClientMeetingList()
        {
            ServiceResponse<IEnumerable<ClientMeeting>> obj = new ServiceResponse<IEnumerable<ClientMeeting>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select x.ClientId,x.FirstName,x.MiddleName,x.LastName,x.Contact,IsNUll(z.EmpId,0) as EmpId,p.FirstName +' ' + ISNULL(p.MiddleName,' ')+' ' + p.LastName " +
                    "as EmpName,IsNUll(y.MeetingId,0) as MeetingId,y.MeetingDate,y.StartTime,y.EndTime from tblClient x Left Join tblMeeting y on x.ClientId=y.ClientId and y.IsStatus<>0 Left join" +
                    " tblEmpClientMeeting z on y.MeetingId=z.MeetingId Left join tblEmployee p on z.EmpId=p.EmpId ;";

                var result = (await connection.QueryAsync(sql)).ToList();


                //Using Query Syntax
                var GroupByQS = from mom in result
                                group mom by new { mom.ClientId, mom.FirstName, mom.MiddleName, mom.LastName, mom.Contact, } into momGroup
                                orderby momGroup.Key.ClientId descending
                                select new ClientMeeting
                                {
                                    ClientId = momGroup.Key.ClientId,
                                    FirstName = momGroup.Key.FirstName,
                                    MiddleName = momGroup.Key.MiddleName,
                                    LastName = momGroup.Key.LastName,
                                    Contact = momGroup.Key.Contact,
                                    Meetings = momGroup.Where(x=>x.MeetingId!=0).Select(x => new ClMeeting
                                    {
                                        EmpId = x.EmpId,
                                        EmpName = x.EmpName!=null? x.EmpName :"",
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

        public async Task<ServiceResponse<MeetingView>> GetMeetingDetail(long meetingId)
        {
            ServiceResponse<MeetingView> obj = new ServiceResponse<MeetingView>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select p.*,q.Contact,q.FirstName as cltFName,q.MiddleName as cltMName,q.LastName as cltLName,r.EmpId,s.FirstName as empFName," +
                    "s.MiddleName as empMName,s.LastName as empLName,s.CellPhone,t.Owner as empOwner,t.FlatNo as empFlatNo,t.Address as empAddress,t.City as empCity," +
                    "t.Country as empCountry,t.State as empState,t.ZipCode as empZipCode,u.MeetingPoint from tblMeeting p inner join tblClient q on p.ClientId=q.ClientId " +
                    "inner join tblEmpClientMeeting r on p.MeetingId=r.MeetingId inner join tblEmployee  s on r.EmpId=s.EmpId left join  tblAddress t " +
                    "on s.EmpId=t.EmpId Left Join tblMeetingPoint u on r.MeetingId=u.MeetingId where p.MeetingId=@MeetingId";

                var rsData = (await connection.QueryAsync(sql, new { @MeetingId = meetingId }));

                MeetingView objResult = (from mom in rsData

                                         group mom by new { mom.MeetingId} into momGroup
                                         orderby momGroup.Key descending


                                         select new MeetingView
                                         {
                                             
                                             MeetingId = momGroup.Key.MeetingId,
                                             MeetingDate = ((DateTime)momGroup.FirstOrDefault().MeetingDate).ToString("dd-MMM-yy"),
                                             StartTime = ((TimeSpan)momGroup.FirstOrDefault().StartTime).TimeHelper(),
                                             EndTime = ((TimeSpan)momGroup.FirstOrDefault().EndTime).TimeHelper(),
                                             Employee = new UserView()
                                             {

                                                 Id = momGroup.FirstOrDefault().EmpId,
                                                 FirstName = momGroup.FirstOrDefault().empFName,
                                                 MiddleName = momGroup.FirstOrDefault().empMName,
                                                 Lastname = momGroup.FirstOrDefault().empLName,
                                                 CellPhone = momGroup.FirstOrDefault().CellPhone,
                                                 Address = new AddressView()
                                                 {
                                                     LocationDetail = momGroup.FirstOrDefault().empAddress,
                                                     FlatNo = momGroup.FirstOrDefault().empFlatNo,
                                                     Country = momGroup.FirstOrDefault().empCountry,
                                                     State = momGroup.FirstOrDefault().empState,
                                                     City = momGroup.FirstOrDefault().empCity,
                                                     ZipCode = momGroup.FirstOrDefault().empZipCode,
                                                 },
                                             },
                                             Client = new UserView()
                                             {

                                                 Id = momGroup.FirstOrDefault().ClientId,
                                                 FirstName = momGroup.FirstOrDefault().cltFName,
                                                 MiddleName = momGroup.FirstOrDefault().cltMName,
                                                 Lastname = momGroup.FirstOrDefault().cltLName,
                                             },
                                             IsStatus= momGroup.FirstOrDefault().IsStatus,
                                             Notes = momGroup.Select(x=>(string)x.MeetingPoint).ToList()
                                         }).FirstOrDefault();

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<string>> UpdateMeeting(MeetingModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string updateQuery = "Update tblMeeting SET ClientId=@ClientId,StartTime=@StartTime,EndTime=@EndTime Where MeetingId=@MeetingId";

                    var result = cnn.Execute(updateQuery, new
                    {
                        _model.ClientId,
                        _model.StartTime,
                        _model.EndTime,
                        _model.MeetingId,
                    });

                    if (result > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Sucessfully  Updated.";
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

        public async Task<ServiceResponse<string>> PostNote(NotesModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string addQuery = "INSERT INTO tblMeetingPoint (MeetingId,MeetingPoint,CreatedOn,CreatedBy) VALUES (@MeetingId,@MeetingPoint,@CreatedOn,@CreatedBy)";

                    var result = cnn.Execute(addQuery, _model);

                    if (result > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Sucessfully  added.";
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

        public async Task<ServiceResponse<string>> ChangeStatus(MeetingStatus _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string addQuery = "Update tblMeeting SET IsStatus= @IsStatus Where MeetingId=@MeetingId";

                    var result = cnn.Execute(addQuery, new { _model.IsStatus, _model.MeetingId });
                    if (result > 0)
                    {

                        if (!string.IsNullOrEmpty(_model.MeetingNote))
                        {
                            string query = "INSERT INTO tblMeetingPoint (MeetingId,MeetingPoint,CreatedOn,CreatedBy) VALUES (@MeetingId,@MeetingPoint,@CreatedOn,@CreatedBy)";
                            var rs = cnn.Execute(query, new { _model.MeetingId, MeetingPoint= _model.MeetingNote, _model.CreatedOn, _model.CreatedBy });
                            sres.Result = true;

                        }

                    }

                    if (result > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Sucessfully  updated.";
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

    }
}
