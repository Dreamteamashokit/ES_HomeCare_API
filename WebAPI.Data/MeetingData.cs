
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
using ES_HomeCare_API.Model;
using ES_HomeCare_API.ViewModel.Meeting;

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
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();
                    string _query = "Insert Into tblMeeting (ClientId,MeetingDate,StartTime,EndTime,IsStatus,CreatedOn,CreatedBy,EmpId)" +
                  " values(@ClientId,@MeetingDate,@StartTime,@EndTime,@IsStatus,@CreatedOn,@CreatedBy,@EmpId) select SCOPE_IDENTITY();";
                    _model.MeetingId = (int)(cnn.ExecuteScalar<int>(_query, _model, transaction));

                    if (!string.IsNullOrEmpty(_model.MeetingNote))
                    {
                        string addComment = "INSERT INTO tblMeetingPoint (MeetingId,MeetingPoint,CreatedOn,CreatedBy) VALUES (@MeetingId,@MeetingNote,@CreatedOn,@CreatedBy)";
                        int rowsAffected = cnn.Execute(addComment, _model, transaction);

                        if (rowsAffected > 0)
                        {
                            sres.Result = true;
                            sres.Data = "Sucessfully  Created.";
                        }
                        else
                        {
                            sres.Result = false;
                            sres.Data = null;
                            sres.Message = "Failed new creation.";
                        }

                        transaction.Commit();
                    }
                    else
                    {

                        sres.Result = true;
                        sres.Data = "Sucessfully  Created.";
                        transaction.Commit();
                    }



                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                sres.Result = false;
                sres.Message = ex.Message;
                return sres;
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }
            return sres;
        }



        public async Task<ServiceResponse<string>> AddRecurringMeeting(MeetingModel model)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sql = "MeetingProc";
                    var sqlParameter = new
                    {
                        @flag = 1,
                        @ClientId = model.ClientId,
                        @EmpId = model.EmpId,
                        @FromDate = model.FromDate,
                        @ToDate = model.ToDate,
                        @StartTime = model.StartTime,
                        @EndTime = model.EndTime,
                        @MeetingPoint = model.MeetingNote,
                        @IsStatus = model.IsStatus,
                        @CreatedOn = model.CreatedOn,
                        @CreatedBy = model.CreatedBy,
                    };
                    int rowsAffected = (await connection.ExecuteAsync(sql, sqlParameter, commandType: CommandType.StoredProcedure));

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        obj.Result = false;
                        obj.Data = null;
                        obj.Message = "Failed new creation.";
                    }
                }
                return obj;

            }
            catch (Exception ex)
            {

                obj.Result = false;
                obj.Message = ex.Message;
                return obj;
            }

        }




        public async Task<ServiceResponse<string>> UpdateMeeting(MeetingModel model)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sql = "MeetingProc";
                    var sqlParameter = new
                    {
                        @flag = (model.MeetingId == 0 ? 2 : 3),
                        @MeetingId = model.MeetingId,
                        @ClientId = model.ClientId,
                        @EmpId = model.EmpId,
                        @MeetingDate = model.MeetingDate,
                        @FromDate = model.FromDate,
                        @ToDate = model.ToDate,
                        @StartTime = model.StartTime,
                        @EndTime = model.EndTime,
                        @MeetingPoint = model.MeetingNote,
                        @IsStatus = model.IsStatus,
                        @CreatedOn = model.CreatedOn,
                        @CreatedBy = model.CreatedBy,
                    };
                    int rowsAffected = (await connection.ExecuteAsync(sql, sqlParameter, commandType: CommandType.StoredProcedure));

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        obj.Result = false;
                        obj.Data = null;
                        obj.Message = "Failed new creation.";
                    }
                }
                return obj;

            }
            catch (Exception ex)
            {

                obj.Result = false;
                obj.Message = ex.Message;
                return obj;
            }

        }

        public async Task<ServiceResponse<IEnumerable<EmpMeeting>>> GetEmpMeetingList(int empId)
        {
            ServiceResponse<IEnumerable<EmpMeeting>> obj = new ServiceResponse<IEnumerable<EmpMeeting>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"select  p.LastName  +' '+ p.FirstName as EmpName,q.*,r.LastName +' '+r.FirstName as ClientName
from tblUser p inner join tblMeeting q on p.UserId=q.EmpId inner join tblUser r 
on q.ClientId=r.UserId where q.EmpId=@UserId and q.IsStatus<>0";

                IEnumerable<EmpMeeting> cmeetings = (await connection.QueryAsync<EmpMeeting>(sql,
                       new { @UserId = empId }));

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
                string sql = @"select  p.LastName  +' '+ p.FirstName as EmpName,q.*,r.LastName +' '+r.FirstName as ClientName
                            from tblUser p RIGHT join tblMeeting q on p.UserId=q.empId inner join tblUser r 
                            on q.ClientId=r.UserId where q.ClientId=@UserId and q.IsStatus<>0";

                IEnumerable<EmpMeeting> cmeetings = (await connection.QueryAsync<EmpMeeting>(sql,
                       new { @UserId = _userId }));

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
                string sql = @"Select x.UserId as ClientId,x.FirstName,x.MiddleName,x.LastName,x.CellPhone,
IsNUll(y.MeetingId,0) as MeetingId,y.MeetingDate,y.StartTime,y.EndTime,     
y.EmpId as EmpId,p.FirstName +' ' + ISNULL(p.MiddleName,' ')+' ' + p.LastName as EmpName
from tblUser x inner join tblClient xx on x.UserId=xx.UserId
Left Join tblMeeting y on xx.UserId=y.ClientId and y.IsStatus<>0  Left join tblUser p on y.EmpId=p.UserId    
Left join tblAddress xy on x.UserId=xy.UserId ORDER BY TRIM(x.LastName)";

                var result = (await connection.QueryAsync(sql)).ToList();


                //Using Query Syntax
                var GroupByQS = (from mom in result
                                 group mom by new { mom.ClientId, mom.FirstName, mom.MiddleName, mom.LastName, mom.CellPhone, } into momGroup
                                 orderby momGroup.Key.ClientId descending
                                 select new ClientMeeting
                                 {
                                     ClientId = momGroup.Key.ClientId,
                                     FirstName = momGroup.Key.FirstName,
                                     MiddleName = momGroup.Key.MiddleName,
                                     LastName = momGroup.Key.LastName,
                                     Contact = momGroup.Key.CellPhone,
                                     Meetings = momGroup.Where(x => x.MeetingId != 0).Select(x => new ClMeeting
                                     {
                                         EmpId = x.EmpId != null ? x.EmpId : 0,
                                         EmpName = x.EmpName != null ? x.EmpName : "Test Name",
                                         MeetingId = x.MeetingId,
                                         MeetingDate = x.MeetingDate,
                                         StartTime = ((TimeSpan)x.StartTime).TimeHelper(),
                                         EndTime = ((TimeSpan)x.EndTime).TimeHelper(),
                                     })
                                 }).OrderBy(x => x.LastName.Trim());


                obj.Data = GroupByQS;
                obj.Result = GroupByQS.Any() ? true : false;
                obj.Message = GroupByQS.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<ClientMeeting>>> GetClientMeetingList(ClientFilter model)
        {
            ServiceResponse<IEnumerable<ClientMeeting>> obj = new ServiceResponse<IEnumerable<ClientMeeting>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "ClientProc";
                var sqlParameter = new { @flag = 1, @IsActive = model.Status, @SupervisorId = model.Coordinator, @State = model.State };
                var result = (await connection.QueryAsync(sql, sqlParameter, commandType: CommandType.StoredProcedure)).ToList();
                //Using Query Syntax
                var GroupByQS = (from mom in result
                                 group mom by new { mom.ClientId, mom.FirstName, mom.MiddleName, mom.LastName, mom.CellPhone, } into momGroup
                                 orderby momGroup.Key.ClientId descending
                                 select new ClientMeeting
                                 {
                                     ClientId = momGroup.Key.ClientId,
                                     FirstName = momGroup.Key.FirstName,
                                     MiddleName = momGroup.Key.MiddleName,
                                     LastName = momGroup.Key.LastName,
                                     Contact = momGroup.Key.CellPhone,
                                     Meetings = momGroup.Where(x => x.MeetingId != 0).Select(x => new ClMeeting
                                     {
                                         EmpId = x.EmpId != null ? x.EmpId : 0,
                                         EmpName = x.EmpName != null ? x.EmpName : "Test Name",
                                         MeetingId = x.MeetingId,
                                         MeetingDate = x.MeetingDate,
                                         StartTime = ((TimeSpan)x.StartTime).TimeHelper(),
                                         EndTime = ((TimeSpan)x.EndTime).TimeHelper(),
                                     })
                                 }).OrderBy(x => x.LastName.Trim());


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
                string sql = @"Select p.*,q.MeetingPoint,r.UserType,
r.FirstName,r.MiddleName,r.LastName,r.Email,r.CellPhone,r.HomePhone,r.EmgPhone,r.EmgContact,
rp.Owner,rp.FlatNo,rp.Address,rp.City,rp.Country,rp.State,rp.ZipCode,
p.EmpId,s.UserType as empUserType,s.FirstName as empFName,s.MiddleName as empMName,s.LastName as empLName,s.Email as empEmail,
s.CellPhone as empCellPhone,s.HomePhone as empHomePhone,s.EmgPhone as empEmgPhone,s.EmgContact as empEmgContact,
sp.Owner as empOwner,sp.FlatNo as empFlatNo,sp.Address as empAddress,
sp.City as empCity,sp.Country as empCountry,sp.State as empState,sp.ZipCode as empZipCode from 
tblMeeting p Left Join tblMeetingPoint q on p.MeetingId=q.MeetingId
Inner Join tblUser r on p.ClientId=r.UserId Left Join tblAddress rp on r.UserId=rp.UserId
Left Join tblUser s on p.EmpId=s.UserId  Left Join tblAddress sp on s.UserId=sp.UserId
Where p.MeetingId=@MeetingId;";

                var rsData = (await connection.QueryAsync(sql, new { @MeetingId = meetingId }));

                MeetingView objResult = (from mom in rsData

                                         group mom by new { mom.MeetingId } into momGroup
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
                                                 // UserType = momGroup.FirstOrDefault().empUserType,
                                                 FirstName = momGroup.FirstOrDefault().empFName,
                                                 MiddleName = momGroup.FirstOrDefault().empMName,
                                                 LastName = momGroup.FirstOrDefault().empLName,
                                                 CellPhone = momGroup.FirstOrDefault().empCellPhone,
                                                 Email = momGroup.FirstOrDefault().empEmail,
                                                 HomePhone = momGroup.FirstOrDefault().empHomePhone,
                                                 EmergPhone = momGroup.FirstOrDefault().empEmgPhone,
                                                 EmergContact = momGroup.FirstOrDefault().empEmgPhone,


                                                 Address = new AddressView()
                                                 {
                                                     LocationDetail = momGroup.FirstOrDefault().empAddress,
                                                     Owner = momGroup.FirstOrDefault().empOwner,
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
                                                 // UserType = momGroup.FirstOrDefault().UserType,
                                                 FirstName = momGroup.FirstOrDefault().FirstName,
                                                 MiddleName = momGroup.FirstOrDefault().MiddleName,
                                                 LastName = momGroup.FirstOrDefault().LastName,
                                                 CellPhone = momGroup.FirstOrDefault().CellPhone,
                                                 Email = momGroup.FirstOrDefault().Email,
                                                 HomePhone = momGroup.FirstOrDefault().HomePhone,
                                                 EmergPhone = momGroup.FirstOrDefault().EmgPhone,
                                                 EmergContact = momGroup.FirstOrDefault().EmgPhone,

                                                 Address = new AddressView()
                                                 {
                                                     LocationDetail = momGroup.FirstOrDefault().Address,
                                                     Owner = momGroup.FirstOrDefault().Owner,
                                                     FlatNo = momGroup.FirstOrDefault().FlatNo,
                                                     Country = momGroup.FirstOrDefault().Country,
                                                     State = momGroup.FirstOrDefault().State,
                                                     City = momGroup.FirstOrDefault().City,
                                                     ZipCode = momGroup.FirstOrDefault().ZipCode,
                                                 },

                                             },
                                             IsStatus = momGroup.FirstOrDefault().IsStatus,
                                             Notes = momGroup.Select(x => (string)x.MeetingPoint).Where(x => x != null).Distinct().ToList()
                                         }).FirstOrDefault();

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
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
                    string addQuery = "Update tblMeeting SET IsStatus= @IsStatus,MeetingCancelReason=@MeetingCanceledReason Where MeetingId=@MeetingId";

                    var result = cnn.Execute(addQuery, new { _model.IsStatus, _model.MeetingCanceledReason, _model.MeetingId });
                    if (result > 0)
                    {

                        if (!string.IsNullOrEmpty(_model.MeetingNote))
                        {
                            string query = "INSERT INTO tblMeetingPoint (MeetingId,MeetingPoint,CreatedOn,CreatedBy) VALUES (@MeetingId,@MeetingPoint,@CreatedOn,@CreatedBy)";
                            var rs = cnn.Execute(query, new { _model.MeetingId, MeetingPoint = _model.MeetingNote, _model.CreatedOn, _model.CreatedBy });
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

        public async Task<ServiceResponse<IEnumerable<MeetingView>>> UpCommingAppointments(int ClientId)
        {
            ServiceResponse<IEnumerable<MeetingView>> obj = new ServiceResponse<IEnumerable<MeetingView>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "ClientProc";
                var sqlParameter = new { @flag = 2, @ClientId = ClientId };
                var result = (await connection.QueryAsync(sql, sqlParameter, commandType: CommandType.StoredProcedure)).Select(mom => new MeetingView
                {
                    MeetingId = mom.MeetingId,
                    MeetingDate = ((DateTime)mom.MeetingDate).ToString("dd-MMM-yy"),
                    StartTime = ((TimeSpan)mom.StartTime).TimeHelper(),
                    EndTime = ((TimeSpan)mom.EndTime).TimeHelper(),
                    Employee = new UserView
                    {
                        Id = mom.EmpId,
                        UserType = mom.TypeName,
                        FirstName = mom.EFirstName,
                        LastName = mom.ELastname,
                        CellPhone = mom.ECellPhone,
                    }
                }).ToList().OrderBy(x => x.MeetingDate.ParseDate("dd-MMM-yy"));
                obj.Data = result;
                obj.Result = result.Any() ? true : false;
                obj.Message = result.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }



        public async Task<ServiceResponse<IEnumerable<MeetingLog>>> GetMeetingLog(long MeetingId)
        {
            ServiceResponse<IEnumerable<MeetingLog>> obj = new ServiceResponse<IEnumerable<MeetingLog>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"Select p.MeetingId ,p.LogMsg,q.FirstName,q.MiddleName,q.LastName,p.CreatedOn from tblMeetingLog p inner join tblUser q on p.CreatedBy=q.UserId
			where p.MeetingId =@MeetingId
			Order by p.CreatedOn";

                var result = (await connection.QueryAsync(sql, new { @MeetingId = MeetingId })).Select(mom => new MeetingLog
                {
                    MeetingId = mom.MeetingId,
                    LogNote = mom.LogMsg,
                    CreatedOn = mom.CreatedOn,
                    CreatedBy = new NameClass
                    {

                        FirstName = mom.FirstName,
                        MiddleName = mom.MiddleName,
                        LastName = mom.LastName,
                    }
                });
                obj.Data = result;
                obj.Result = result.Any() ? true : false;
                obj.Message = result.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }


        public async Task<ServiceResponse<string>> AddUpdateMeetingRate(MeetingRateModel model)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sql = "addUpdateMeetingRate";
                    var sqlParameter = new
                    {
                        @MeetingRateId = model.MeetingRateId,
                        @MeetingId = model.MeetingId,
                        @BillingCode = model.BillingCode,
                        @BillingUnits = model.BillingUnits,
                        @BillingRate = model.BillingRate,
                        @BillingTotal = model.BillingTotal,
                        @BillingStatus = model.BillingStatus,
                        @BillingTravelTime = model.BillingTravelTime,
                        @PayrollUnitsPaid = model.PayrollUnitsPaid,
                        @PayrollPayRate = model.PayrollPayRate,
                        @PayrollPayTotal = model.PayrollPayTotal,
                        @PayrollPayStatus = model.PayrollPayStatus,
                        @PayrollMileage = model.PayrollMileage,
                        @PayrollPublicTrans = model.PayrollPublicTrans,
                        @PayrollMisc = model.PayrollMisc,
                        @PayrollDoNotPay = model.PayrollDoNotPay,
                        @SentPayrollDate = string.IsNullOrWhiteSpace(model.SentPayrollDate) ? "" : model.SentPayrollDate,
                        @IsActive = true,
                        @CreatedBy = model.CreatedBy
                    };
                    int rowsAffected = (await connection.ExecuteAsync(sql, sqlParameter, commandType: CommandType.StoredProcedure));
                    obj.Result = true;
                    obj.Data = "Sucessfully  Created.";
                }
                return obj;

            }
            catch (Exception ex)
            {

                obj.Result = false;
                obj.Message = ex.Message;
                return obj;
            }

        }


        public async Task<ServiceResponse<MeetingRateViewModel>> GetMeetingRateByMeetingId(long MeetingId)
        {
            ServiceResponse<MeetingRateViewModel> obj = new ServiceResponse<MeetingRateViewModel>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"SELECT * FROM tblMeetingRate  Where MeetingId = @MeetingId";

                var result = (await connection.QueryAsync(sql, new { @MeetingId = MeetingId })).Select(mom => new MeetingRateViewModel
                {
                    MeetingRateId = mom.MeetingRateId,
                    MeetingId = mom.MeetingId,
                    BillingCode = mom.BillingCode,
                    BillingRate = mom.BillingRate,
                    BillingStatus = mom.BillingStatus,
                    BillingTotal = mom.BillingTotal,
                    BillingTravelTime = mom.BillingTravelTime,
                    BillingUnits = mom.BillingUnits,
                    IsActive = mom.IsActive,
                    PayrollDoNotPay = mom.PayrollDoNotPay,
                    PayrollMileage = mom.PayrollMileage,
                    PayrollMisc = mom.PayrollMisc,
                    PayrollPayRate = mom.PayrollPayRate,
                    PayrollPayStatus = mom.PayrollPayStatus,
                    PayrollPayTotal = mom.PayrollPayTotal,
                    PayrollPublicTrans = mom.PayrollPublicTrans,
                    PayrollUnitsPaid = mom.PayrollUnitsPaid,
                    SentPayrollDate = mom.SentPayrollDate

                }).FirstOrDefault();
                obj.Data = result;
                obj.Result = result != null ? true : false;
                obj.Message = result != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }

    }
}
