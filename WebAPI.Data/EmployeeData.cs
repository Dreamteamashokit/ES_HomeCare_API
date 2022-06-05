using Dapper;
using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.ViewModel.Employee;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Data.IData;

namespace WebAPI_SAMPLE.WebAPI.Data
{
    public class EmployeeData : IEmployeeData
    {
        private IConfiguration configuration;
        public EmployeeData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #region Employee
        public async Task<ServiceResponse<int>> AddEmployee(EmployeeModel _model)
        {
            ServiceResponse<int> sres = new ServiceResponse<int>();
            IDbTransaction transaction = null;

            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();

                    string _query = "INSERT INTO tblUser (UserKey,UserType,UserName,UserPassword,SSN,FirstName,MiddleName,LastName,DOB,Email,CellPhone,HomePhone,EmgPhone,EmgContact,Gender,MaritalStatus,Ethnicity,SupervisorId,IsActive,CreatedOn,CreatedBy) VALUES (@UserKey,@UserType,@UserName,@UserPassword,@SSN,@FirstName,@MiddleName,@LastName,@DOBS,@Email,@CellPhone,@HomePhone,@EmgPhone,@EmgContact,@Gender,@MaritalStatus,@Ethnicity,@SupervisorId,@IsActive,@CreatedOn,@CreatedBy); select SCOPE_IDENTITY();";


                    _model.UserId = (int)(cnn.ExecuteScalar<int>(_query, _model, transaction));

                    string sqlQuery = "INSERT INTO tblEmployee (UserId,EmpType,DateOfHire,DateOfFirstCase,Dependents,City,Country,TaxState,ZipCode,Municipality,Notes,IsActive,CreatedOn,CreatedBy) VALUES (@UserId,@EmpType,@DateOfHireS,@DateOfFirstCaseS,@Dependents,@City,@Country,@TaxState,@ZipCode,@Municipality,@Notes,@IsActive,@CreatedOn,@CreatedBy)";

                    int rowsAffected = cnn.Execute(sqlQuery, _model, transaction);
                    transaction.Commit();
                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = _model.UserId;
                    }
                    else
                    {
                        sres.Data = 0;
                        sres.Message = "Failed new creation.";
                    }

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
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

        public async Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(int userId)
        {
            ServiceResponse<IEnumerable<EmployeeList>> obj = new ServiceResponse<IEnumerable<EmployeeList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                var procedure = "[EmpProc]";
                var values = new { @flag = 1, @IsActive = 1, @UserId = userId };
                IEnumerable<EmployeeList> results = (await connection.QueryAsync<EmployeeList>(procedure,
        values, commandType: CommandType.StoredProcedure)).OrderBy(x => x.EmpName.Trim()); 
                obj.Data = results;
                obj.Result = results.Any() ? true : false;
                obj.Message = results.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(ClientFilter model)
        {
            ServiceResponse<IEnumerable<EmployeeList>> obj = new ServiceResponse<IEnumerable<EmployeeList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlQuery = "EmpProc";
                var sqlParameter = new { @flag = 3, @IsActive = model.Status, 
                    @SupervisorId = model.Coordinator, 
                    @State = model.State, 
                    @TypeId=model.EmpType };
               
                IEnumerable<EmployeeList> results = (await connection.QueryAsync<EmployeeList>(sqlQuery,
    sqlParameter, commandType: CommandType.StoredProcedure)).OrderBy(x => x.EmpName.Trim()); 
                obj.Data = results;
                obj.Result = results.Any() ? true : false;
                obj.Message = results.Any() ? "Data Found." : "No Data found.";

            
            }
            return obj;
        }

        public async Task<ServiceResponse<string>> DeleteEmployee(int UserId)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var procedure = "[EmpProc]";
                    var values = new { @flag = 2, @IsActive = 0, @UserId = UserId };
                    int rowsAffected = connection.Execute(procedure, values, commandType: CommandType.StoredProcedure);

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Employee Deleted";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Deletion Failed.";
                    }

                }

            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }


            return obj;
        }

        public async Task<ServiceResponse<EmployeeJson>> GetEmployeeById(int UserId)
        {
            ServiceResponse<EmployeeJson> obj = new ServiceResponse<EmployeeJson>();

            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "SELECT x.*,y.EmpType,y.DateOfHire,y.DateOfFirstCase,y.Dependents,y.City,y.Country,y.TaxState,y.ZipCode,y.Municipality,y.Notes,p.ItemName as GenderName,q.ItemName as MaritalStatusName,r.ItemName as EthnicityName,s.FirstName+''+s.LastName as Supervisor,t.TypeName as EmpTypeName FROM tblUser x inner join tblEmployee y on x.UserId=y.UserId Left join tblMaster p on x.Gender=p.MasterId Left join tblMaster q on x.MaritalStatus=q.MasterId Left join tblMaster r on x.Ethnicity=r.MasterId Left join tblUser s on  x.SupervisorId=s.UserId Left join tblEmpType t on  y.EmpType=t.TypeId Where x.UserId=@UserId; ";
                IEnumerable<EmployeeJson> cmeetings = (await connection.QueryAsync<EmployeeJson>(sql,
                         new { @UserId = UserId })).ToList().OrderBy(x => x.LastName.Trim()); 
                obj.Data = cmeetings.FirstOrDefault();
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        #endregion



        #region Address

        public async Task<ServiceResponse<string>> AddEmpAddress(AddressModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = String.Empty;
                    if (_model.AddressId > 0)
                    {
                        sqlQuery = "UPDATE [dbo].[tblAddress] SET UserId=@UserId,AddressType=@AddressType,Owner=@Owner,FlatNo=@FlatNo,Address=@Address,City=@City,Country=@Country,State=@State,ZipCode=@ZipCode,Longitude=@Longitude,Latitude=@Latitude,CreatedOn=@CreatedOn,CreatedBy=@CreatedBy WHERE AddressId=@AddressId";
                    }
                    else
                    {
                        sqlQuery = "Insert Into tblAddress (UserId,AddressType,Owner,FlatNo,Address,City,Country,State,ZipCode,Longitude,Latitude,CreatedOn,CreatedBy) Values (@UserId,@AddressType,@Owner,@FlatNo,@Address,@City,@Country,@State,@ZipCode,@Longitude,@Latitude,@CreatedOn,@CreatedBy);";
                    }


                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        @AddressId = _model.AddressId,
                        @UserId = _model.UserId,
                        @AddressType = _model.AddressType,
                        @Owner = _model.Owner,
                        @FlatNo = _model.FlatNo,
                        @Address = _model.Address,
                        @City = _model.City,
                        @Country = _model.Country,
                        @State = _model.State,
                        @ZipCode = _model.ZipCode,
                        @Longitude = _model.Longitude,
                        @Latitude = _model.Latitude,
                        @CreatedOn = _model.CreatedOn,
                        @CreatedBy = _model.CreatedBy
                    });

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

        public async Task<ServiceResponse<AddressModel>> GetEmpAddress(int empId)
        {
            ServiceResponse<AddressModel> obj = new ServiceResponse<AddressModel>();

            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "SELECT * FROM tblAddress Where UserId=@UserId;";
                IEnumerable<AddressModel> cmeetings = (await connection.QueryAsync<AddressModel>(sql,
                         new { @UserId = empId }));
                obj.Data = cmeetings.FirstOrDefault();
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        #endregion


        public async Task<ServiceResponse<string>> AddIncident(IncidentModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblIncident (EmpId,IncidentDate,ClientId,IncidentDetail,CreatedOn,CreatedBy) Values (@EmpId,@IncidentDate,@ClientId,@IncidentDetail,@CreatedOn,@CreatedBy);";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        EmpId = _model.EmpId,
                        ClientId = _model.ClientId,
                        IncidentDate = Convert.ToDateTime(_model.IncidentDate),
                        IncidentDetail = _model.IncidentDetail,
                        CreatedOn = _model.CreatedOn,
                        CreatedBy = _model.CreatedBy
                    });

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

        public async Task<ServiceResponse<IEnumerable<IncidentModel>>> GetIncidentList(int empId)
        {
            ServiceResponse<IEnumerable<IncidentModel>> obj = new ServiceResponse<IEnumerable<IncidentModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sql = "SELECT * FROM tblIncident Where EmpId=@EmpId;";

                IEnumerable<IncidentModel> cmeetings = (await connection.QueryAsync<IncidentModel>(sql,
                         new { EmpId = empId }));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        public async Task<ServiceResponse<string>> AddAttendance(AttendanceModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery;
                    if (_model.EntityId == 0)
                    {

                        sqlQuery = @"Insert Into tblAttendance (EmpId,Reason,StartDate,EndDate,Notes,CreatedOn,CreatedBy,IsActive) 
					Values (@UserId,@Reason,@StartDate,@EndDate,@Notes,@CreatedOn,@CreatedBy,@IsActive);";


                    }
                    else
                    {
                        sqlQuery = @"Update tblAttendance Set Reason=@Reason,StartDate=@StartDate,EndDate=@EndDate,Notes=@Notes,IsActive=@IsActive 
					Where AttendanceId=@AttendanceId";
                    }
                    int rowsAffected =await  db.ExecuteAsync(sqlQuery, new
                    {
                        AttendanceId= _model.EntityId,
                        UserId = _model.UserId,
                        Reason = _model.Reason,
                        StartDate = _model.StartDate.ParseDate(),
                        EndDate = _model.EndDate.ParseDate(),
                        Notes = _model.Notes,
                        CreatedOn = _model.CreatedOn,
                        CreatedBy = _model.CreatedBy,
                        IsActive=  (int)Status.Active
                    });
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
                }

            }
            catch (Exception ex)
            {
                sres.Result = false;
                sres.Data = null;
                sres.Message = ex.Message;
                return sres;
            }
            return sres;
        }

        public async Task<ServiceResponse<IEnumerable<AttendanceModel>>> GetAttendanceList(int empId)
        {
            ServiceResponse<IEnumerable<AttendanceModel>> obj = new ServiceResponse<IEnumerable<AttendanceModel>>();


            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "SELECT * FROM tblAttendance Where EmpId=@EmpId and IsActive=@IsActive;";
                IEnumerable<AttendanceModel> cmeetings = (await connection.QueryAsync<AttendanceModel>(sql,
                         new { @EmpId = empId, @IsActive=(int)Status.Active }));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        public async Task<ServiceResponse<string>> DelAttendance(int AttendanceId)
        {
            ServiceResponse<string> result = new ServiceResponse<string>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                var sqlQuery = "Update tblAttendance Set IsActive=@IsActive where Attendance=@AttendanceId;";
                var modeMapping = new
                {

                    @AttendanceId = AttendanceId,
                    @IsActive = (int)Status.InActive,
                };
                int rowsAffected = await connection.ExecuteAsync(sqlQuery, modeMapping);
                if (rowsAffected > 0)
                {
                    result.Result = true;
                    result.Data = "Sucessfully  Updated.";
                }
                else
                {
                    result.Data = null;
                    result.Message = "Failed to Update.";
                }

            }
            return result;
        }






        public async Task<ServiceResponse<string>> SaveExitEmpStatus(StatusModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblEmpStatus (EmployeeId,TypeId,ScheduleId," +
                        "OfficeUserID,Note,OKResume,ReHire,TextCheck,ScreenCheck,EmailCheck,EffectiveDate,ReturnDate,CreatedOn,CreatedBy) " +
                        "Values(@UserId,@TypeStatusID,@Scheduling,@OfficeUserId,@Note,@Resume,@Rehire,@Text,@Screen," +
                        "@Email,@EffectiveDate,@ReturnDate,@CreatedOn,@CreatedBy)";

                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        UserId = _model.UserId,
                        TypeStatusId = _model.TypeStatusId,
                        Scheduling = _model.Scheduling,
                        OfficeUserId = _model.OfficeUserId,
                        Note = _model.Note,
                        Resume = _model.Resume,
                        Rehire = _model.Rehire,
                        Text = _model.Text,
                        Screen = _model.Screen,
                        Email = _model.Email,
                        EffectiveDate = _model.EffectiveDate,
                        ReturnDate = _model.ReturnDate,
                        CreatedOn = _model.CreatedOn,
                        CreatedBy = _model.CreatedBy

                    });

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

            }

            return sres;
        }

        public async Task<ServiceResponse<IEnumerable<AvailabilityMaster>>> GetAvailabilityList()
        {
            ServiceResponse<IEnumerable<AvailabilityMaster>> obj = new ServiceResponse<IEnumerable<AvailabilityMaster>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "SELECT * FROM tblAvailabilityMaster;";
                IEnumerable<AvailabilityMaster> cmeetings = (await connection.QueryAsync<AvailabilityMaster>(sql));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<AvailabilityStatus>>> GetEmpStatusList(int empId)
        {
            ServiceResponse<IEnumerable<AvailabilityStatus>> obj = new ServiceResponse<IEnumerable<AvailabilityStatus>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select ItemName as StatusType,EffectiveDate,ReturnDate,OKResume as Resume,ReHire as Rehire,Note from tblEmpStatus ES inner join tblMaster ESM  on ES.TypeId = ESM.MasterId  where ES.EmployeeId = @EmployeeId ;";
                IEnumerable<AvailabilityStatus> cmeetings = (await connection.QueryAsync<AvailabilityStatus>(sql, new { EmployeeId = empId }));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<string>> AddCompliance(ComplianceModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblCompliance (EmpId,DueDate,CompletedOn,Category,Code,Result,Nurse,Notes,CreatedOn,CreatedBy) Values (@UserId,@DueDate,@CompletedOn,@Category,@Code,@Result,@Nurse,@Notes,@CreatedOn,@CreatedBy);";

                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        UserId = _model.UserId,
                        DueDate = Convert.ToDateTime(_model.DueDate),
                        CompletedOn = Convert.ToDateTime(_model.CompletedOn),
                        Category = _model.Category,
                        Code = _model.Code,
                        Result = _model.Result,
                        Notes = _model.Notes,
                        Nurse = _model.Nurse,
                        CreatedOn = _model.CreatedOn,
                        CreatedBy = _model.CreatedBy
                    });

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

        public async Task<ServiceResponse<IEnumerable<ComplianceModel>>> GetComplianceList(int empId)
        {
            ServiceResponse<IEnumerable<ComplianceModel>> obj = new ServiceResponse<IEnumerable<ComplianceModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sql = "SELECT * FROM tblCompliance Where EmpId=@EmpId;";

                IEnumerable<ComplianceModel> cmeetings = (await connection.QueryAsync<ComplianceModel>(sql,
                         new { EmpId = empId }));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        public async Task<ServiceResponse<string>> SaveEmpPayRate(EmployeeRateModel client)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {

                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {

                    SqlCommand cmd = new SqlCommand("SP_SaveEmpPayRateProc", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", client.UserId);
                    cmd.Parameters.AddWithValue("@ClientID", client.ClientId);
                    cmd.Parameters.AddWithValue("@EffectiveDate", client.EffectiveDate);
                    cmd.Parameters.AddWithValue("@EndDate", client.EndDate);
                    cmd.Parameters.AddWithValue("@Description", client.Description);
                    cmd.Parameters.AddWithValue("@Note", client.Note);
                    cmd.Parameters.AddWithValue("@Hourly", client.Hourly);
                    cmd.Parameters.AddWithValue("@LiveIn", client.Livein);
                    cmd.Parameters.AddWithValue("@Visit", client.Visit);
                    cmd.Parameters.AddWithValue("@OverHourly", client.OverHourly);
                    cmd.Parameters.AddWithValue("@OverLiveIn", client.OverLivein);
                    cmd.Parameters.AddWithValue("@OverVisit", client.OverVisit);
                    cmd.Parameters.AddWithValue("@IncludedHour", client.OptionalHour);
                    cmd.Parameters.AddWithValue("@AdditionalHour", client.OptionalAddHour);
                    cmd.Parameters.AddWithValue("@Mileage", client.Mileage);
                    cmd.Parameters.AddWithValue("@TravelTime", client.TravelTime);
                    cmd.Parameters.AddWithValue("@Gas", client.Gas);
                    cmd.Parameters.AddWithValue("@Extra", client.Extra);
                    //cmd.Parameters.AddWithValue("@Casetype", client.ca);
                    //cmd.Parameters.AddWithValue("@Result", client.EmgContact);
                    cmd.Parameters.AddWithValue("@CreatedOn", client.CreatedOn);
                    cmd.Parameters.AddWithValue("@createdBy", client.CreatedBy);

                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        sres.Result = true;
                        sres.Data = "New Employee Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new employee creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
            return sres;
        }

        public async Task<ServiceResponse<List<EmpRate>>> GetEmpPayRate(long EmpId, long ClientId)
        {

            ServiceResponse<List<EmpRate>> obj = new ServiceResponse<List<EmpRate>>();
            List<EmpRate> emp = new List<EmpRate>();

            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_GetPayRateProc", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                    cmd.Parameters.AddWithValue("@ClientID", ClientId);
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            emp.Add(new EmpRate
                            {
                                Discription = table.Rows[i]["Description"].ToString(),
                                EffectiveDates = table.Rows[i]["EffectiveDate"].ToString(),
                                OverTimeRate = table.Rows[i]["OverTime"].ToString(),
                                Ragularrate = table.Rows[i]["Regulartime"].ToString()
                            });

                        }
                        obj.Result = true;
                    }
                    obj.Data = emp;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
            finally
            {

            }
        }

        public async Task<ServiceResponse<string>> SaveEmpDeclinedCase(EmpDeclinedCase client)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {

                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {

                    SqlCommand cmd = new SqlCommand("SP_SaveEmpDeclinedCaseProc", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", client.UserId);

                    cmd.Parameters.AddWithValue("@ReportedDate", client.RepotedDate);
                    cmd.Parameters.AddWithValue("@ClientID", client.ClientId);
                    cmd.Parameters.AddWithValue("@caseid", client.Casetypeid);
                    cmd.Parameters.AddWithValue("@DeclinedReason", client.DeclineReason);
                    cmd.Parameters.AddWithValue("@AssignmentStartDate", client.AssignmentStart);
                    cmd.Parameters.AddWithValue("@Note", client.Note);
                    cmd.Parameters.AddWithValue("@Day", client.Day);
                    cmd.Parameters.AddWithValue("@Week", client.Week);
                    cmd.Parameters.AddWithValue("@CreatedOn", client.CreatedOn);
                    cmd.Parameters.AddWithValue("@createdBy", client.CreatedBy);

                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        sres.Result = true;
                        sres.Data = "New Employee Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new employee creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
            return sres;
        }

        public async Task<ServiceResponse<List<EmpDeclinedCase>>> GetEmpDeclinedcase(int EmpId)
        {

            ServiceResponse<List<EmpDeclinedCase>> obj = new ServiceResponse<List<EmpDeclinedCase>>();
            List<EmpDeclinedCase> emp = new List<EmpDeclinedCase>();

            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_GetDeclinedCaseProc", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            emp.Add(new EmpDeclinedCase
                            {
                                RepotedDate = table.Rows[i]["ReportedDate"].ToString(),
                                ClientName = table.Rows[i]["Name"].ToString(),
                                CasetypeName = table.Rows[i]["CaseType"].ToString(),
                                DeclineReason = table.Rows[i]["DeclinedReason"].ToString(),
                                Day = Convert.ToInt16(table.Rows[i]["Day"].ToString()),
                                Week = Convert.ToInt16(table.Rows[i]["Week"].ToString()),
                                AssignmentStart = table.Rows[i]["AssignmentStartDate"].ToString()
                            });

                        }
                        obj.Result = true;
                    }
                    obj.Data = emp;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
            finally
            {

            }
        }


        public async Task<ServiceResponse<CaregiverViewModel>> GetCareGiverDetails(int UserId)
        {
            ServiceResponse<CaregiverViewModel> obj = new ServiceResponse<CaregiverViewModel>();
            CaregiverViewModel caregiver = new CaregiverViewModel();

            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetCaregiverDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        address address = null;
                        if (ds.Tables.Count > 1)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                address = new address
                                {
                                    addressLine1 = ds.Tables[1].Rows[i]["addressLine1"].ToString(),
                                    addressLine2 = ds.Tables[1].Rows[i]["addressLine2"].ToString(),
                                    state = ds.Tables[1].Rows[i]["state"].ToString(),
                                    city = ds.Tables[1].Rows[i]["city"].ToString(),
                                    zipcode = ds.Tables[1].Rows[i]["zipcode"].ToString()
                                };
                            }
                        }
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            caregiver = new CaregiverViewModel
                            {
                                providerTaxId = ds.Tables[0].Rows[i]["providerTaxId"].ToString(),
                                npi = Convert.ToInt32(ds.Tables[0].Rows[i]["npi"].ToString()),
                                ssn = Convert.ToInt32(ds.Tables[0].Rows[i]["ssn"].ToString()),
                                dateOfBirth = Convert.ToDateTime(ds.Tables[0].Rows[i]["dateOfBirth"].ToString()),
                                email = ds.Tables[0].Rows[i]["email"].ToString(),
                                externalID = ds.Tables[0].Rows[i]["externalID"].ToString(),
                                firstName = ds.Tables[0].Rows[i]["firstName"].ToString(),
                                lastName = ds.Tables[0].Rows[i]["lastName"].ToString(),
                                gender = ds.Tables[0].Rows[i]["gender"].ToString(),
                                hireDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["gender"].ToString()),
                                phoneNumber = ds.Tables[0].Rows[i]["phoneNumber"].ToString(),
                                professionalLicenseNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["professionalLicenseNumber"].ToString()),
                                qualifier = ds.Tables[0].Rows[i]["qualifier"].ToString(),
                                stateRegistrationID = Convert.ToInt32(ds.Tables[0].Rows[i]["stateRegistrationID"].ToString()),
                                type = ds.Tables[0].Rows[i]["type"].ToString(),
                                address = address
                            };
                        }
                        obj.Result = true;
                    }
                    obj.Data = caregiver;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }

        public async Task<ServiceResponse<ExternalLoginViewModel>> ExternalLogin(ExternalLoginModel externalLoginModel)
        {
            ServiceResponse<ExternalLoginViewModel> obj = new ServiceResponse<ExternalLoginViewModel>();
            ExternalLoginViewModel result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("ExternalLoginBySSNORMobile", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SSN", externalLoginModel.SSN);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                result = new ExternalLoginViewModel
                                {
                                    userId = Convert.ToInt32(ds.Tables[0].Rows[i]["userId"].ToString()),
                                    userTypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["userTypeId"]),
                                    firstName = ds.Tables[0].Rows[i]["firstName"].ToString(),
                                    lastName = ds.Tables[0].Rows[i]["lastName"].ToString(),
                                    middleName = ds.Tables[0].Rows[i]["middleName"].ToString(),
                                    userName = ds.Tables[0].Rows[i]["userName"].ToString(),
                                    email = ds.Tables[0].Rows[i]["email"].ToString(),
                                    latitude = Convert.ToDecimal(ds.Tables[0].Rows[i]["latitude"]),
                                    longitude = Convert.ToDecimal(ds.Tables[0].Rows[i]["longitude"])
                                };
                            }
                        }
                        obj.Result = true;
                    }
                    obj.Data = result;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }

        public async Task<ServiceResponse<List<ClientListViewModel>>> GetClientListByempId(int empId)
        {
            ServiceResponse<List<ClientListViewModel>> obj = new ServiceResponse<List<ClientListViewModel>>();
            List<ClientListViewModel> objClientList = null;
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetCLientDetailsByUserId", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empId", empId);

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        objClientList = new List<ClientListViewModel>();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ClientListViewModel objClient = new ClientListViewModel
                            {
                                ClientId = Convert.ToInt32(ds.Tables[0].Rows[i]["ClientId"]),
                                UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]),
                                FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString(),
                                MiddleName = ds.Tables[0].Rows[i]["MiddleName"].ToString(),
                                LastName = ds.Tables[0].Rows[i]["LastName"].ToString(),
                                MeetingDate = ds.Tables[0].Rows[i]["MeetingDate"].ToString(),
                                MeetingStartTime = ds.Tables[0].Rows[i]["StartTime"].ToString(),
                                MeetingEndTime = ds.Tables[0].Rows[i]["EndTime"].ToString(),
                                FlatNo = ds.Tables[0].Rows[i]["FlatNo"].ToString(),
                                City = ds.Tables[0].Rows[i]["City"].ToString(),
                                Address = ds.Tables[0].Rows[i]["Address"].ToString(),
                                Country = ds.Tables[0].Rows[i]["Country"].ToString(),
                                State = ds.Tables[0].Rows[i]["State"].ToString(),
                                ZipCode = ds.Tables[0].Rows[i]["ZipCode"].ToString(),
                                Latitude = ds.Tables[0].Rows[i]["Latitude"].ToString(),
                                Longitude = ds.Tables[0].Rows[i]["Longitude"].ToString()
                            };
                            objClientList.Add(objClient);
                        }
                        obj.Result = true;
                    }
                    obj.Data = objClientList;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }

        public async Task<ServiceResponse<HHAClockInDetailsModel>> GetClockinDetails(int userId)
        {
            ServiceResponse<HHAClockInDetailsModel> obj = new ServiceResponse<HHAClockInDetailsModel>();
            HHAClockInDetailsModel objClockinDetail = null;
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetHHAClockInOutDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            objClockinDetail = new HHAClockInDetailsModel();

                            objClockinDetail.ClockId = Convert.ToInt32(ds.Tables[0].Rows[i]["ClockId"]);
                            objClockinDetail.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                            if (ds.Tables[0].Rows[i]["ClockInTime"] != null)
                            {
                                objClockinDetail.ClockInTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["ClockInTime"]);
                            }
                            if (ds.Tables[0].Rows[i]["ClockOutTime"] != null && ds.Tables[0].Rows[i]["ClockOutTime"] != DBNull.Value)
                            {
                                objClockinDetail.ClockOutTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["ClockOutTime"]);
                            }
                            objClockinDetail.Notes = ds.Tables[0].Rows[i]["Notes"].ToString();
                        }
                        obj.Result = true;
                    }
                    obj.Data = objClockinDetail;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }

        public async Task<ServiceResponse<string>> HHAClockin(HHAClockInModel hhaClockin)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("AddHHAClockInOutDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", hhaClockin.UserId);
                    
                    cmd.Parameters.AddWithValue("@ClockInTime", Convert.ToDateTime(hhaClockin.ClockInTime));
                    if (hhaClockin.Type == 2)
                    {
                        cmd.Parameters.AddWithValue("@ClockOutTime",Convert.ToDateTime(hhaClockin.ClockOutTime));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ClockOutTime", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@Notes", hhaClockin.Notes == null ? "" : hhaClockin.Notes);
                    cmd.Parameters.AddWithValue("@BedBath", hhaClockin.BedBath);
                    cmd.Parameters.AddWithValue("@SpongeBath", hhaClockin.SpongeBath);
                    cmd.Parameters.AddWithValue("@Footcare", hhaClockin.Footcare);
                    cmd.Parameters.AddWithValue("@Skincare", hhaClockin.Skincare);
                    cmd.Parameters.AddWithValue("@ClientSignature", hhaClockin.ClientSignature == null ? "" : hhaClockin.ClientSignature);
                    cmd.Parameters.AddWithValue("@HHAUserSignature", hhaClockin.HHAUserSignature == null ? "" : hhaClockin.HHAUserSignature);
                    cmd.Parameters.AddWithValue("@Type", hhaClockin.Type);

                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        sres.Result = true;
                        sres.Data = "HHA Login Success.";
                        sres.Message = "HHA Login Success.";
                    }
                    else
                    {
                        sres.Result = true;
                        sres.Data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return sres;
        }
    }
}
