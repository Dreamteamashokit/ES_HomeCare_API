using Dapper;
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
        public async Task<ServiceResponse<string>> AddEmployee(EmployeeModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {

                    string _query = "INSERT INTO tblUser (UserKey,UserType,UserName,UserPassword,SSN,FirstName,MiddleName,LastName,DOB,Email,CellPhone,HomePhone,EmgPhone,EmgContact,Gender,MaritalStatus,Ethnicity,SupervisorId,IsActive,CreatedOn,CreatedBy) VALUES (@UserKey,@UserType,@UserName,@UserPassword,@SSN,@FirstName,@MiddleName,@LastName,@DOB,@Email,@CellPhone,@HomePhone,@EmgPhone,@EmgContact,@Gender,@MaritalStatus,@Ethnicity,@SupervisorId,@IsActive,@CreatedOn,@CreatedBy); select SCOPE_IDENTITY();";
                    _model.UserId = (int)(cnn.Query<int>(_query, _model).First());

                    string sqlQuery = "INSERT INTO tblEmployee (UserId,EmpType,DateOfHire,DateOfFirstCase,Dependents,City,Country,TaxState,ZipCode,Municipality,Notes,IsActive,CreatedOn,CreatedBy) VALUES (@UserId,@EmpType,@DateOfHire,@DateOfFirstCase,@Dependents,@City,@Country,@TaxState,@ZipCode,@Municipality,@Notes,@IsActive,@CreatedOn,@CreatedBy)";


                    int rowsAffected = cnn.Execute(sqlQuery, _model);

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

        public async Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(int userId)
        {
            ServiceResponse<IEnumerable<EmployeeList>> obj = new ServiceResponse<IEnumerable<EmployeeList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                var procedure = "[EmpProc]";
                var values = new { @flag = 1, @IsActive = 1,@UserId= userId };
                IEnumerable<EmployeeList> results = (await connection.QueryAsync<EmployeeList>(procedure,
        values, commandType: CommandType.StoredProcedure));
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
                    var values = new { @flag = 2, @IsActive = 0, @UserId= UserId };
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
                string sql = "SELECT x.UserKey,x.UserType,x.UserName,x.UserPassword,x.SSN,x.FirstName,x.MiddleName,x.LastName,CONVERT(varchar, x.DOB, 105) as DOB,x.Email,x.CellPhone,x.HomePhone,x.EmgPhone,x.EmgContact,x.Gender,x.MaritalStatus,x.Ethnicity,x.SupervisorId,x.IsActive,x.CreatedOn,x.CreatedBy,y.EmpType,CONVERT(varchar, y.DateOfHire, 105) as DateOfHire,CONVERT(varchar, y.DateOfFirstCase, 105) as DateOfFirstCase,y.Dependents,y.City,y.Country,y.TaxState,y.ZipCode,y.Municipality,y.Notes,p.ItemName as GenderName,q.ItemName as MaritalStatusName,r.ItemName as EthnicityName,s.FirstName+''+s.LastName as Supervisor,t.TypeName as EmpTypeName FROM tblUser x inner join tblEmployee y on x.UserId=y.UserId Left join tblMaster p on x.Gender=p.MasterId Left join tblMaster q on x.MaritalStatus=q.MasterId Left join tblMaster r on x.Ethnicity=r.MasterId Left join tblUser s on x.SupervisorId=s.UserId Left join tblEmpType t on  y.EmpType=t.TypeId Where x.UserId=@UserId; ";
                IEnumerable<EmployeeJson> cmeetings = (await connection.QueryAsync<EmployeeJson>(sql,
                         new { @UserId = UserId }));
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
                    string sqlQuery = "Insert Into tblAddress (UserId,AddressType,Owner,FlatNo,Address,City,Country,State,ZipCode,CreatedOn,CreatedBy) Values (@UserId,@AddressType,@Owner,@FlatNo,@Address,@City,@Country,@State,@ZipCode,@CreatedOn,@CreatedBy);";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        UserId = _model.UserId,                       
                        AddressType = _model.AddressType,
                        Owner = _model.Owner,
                        FlatNo = _model.FlatNo,
                        Address = _model.Address,
                        City = _model.City,
                        Country = _model.Country,
                        State = _model.State,
                        ZipCode = _model.ZipCode,
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
                    string sqlQuery = "Insert Into tblAttendance (EmpId,Reason,StartDate,EndDate,Notes,CreatedOn,CreatedBy) Values (@UserId,@Reason,@StartDate,@EndDate,@Notes,@CreatedOn,@CreatedBy);";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        UserId = _model.UserId,
                        Reason = _model.Reason,
                        StartDate = _model.StartDate,
                        EndDate = _model.EndDate,
                        Notes = _model.Notes,
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

        public async Task<ServiceResponse<IEnumerable<AttendanceModel>>> GetAttendanceList(int empId)
        {
            ServiceResponse<IEnumerable<AttendanceModel>> obj = new ServiceResponse<IEnumerable<AttendanceModel>>();


            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "SELECT * FROM tblAttendance Where EmpId=@EmpId;";
                IEnumerable<AttendanceModel> cmeetings = (await connection.QueryAsync<AttendanceModel>(sql,
                         new { @EmpId = empId }));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

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
                string sql = "select ItemName as StatusType,EffectiveDate,ReturnDate,OKResume as Resume,ReHire as Rehire,Note from tblEmpStatus ES inner join tblMaster ESM  on ES.TypeId = ESM.ItemId and ESM.MasterType = 5 where ES.EmployeeId = @EmployeeId ;";
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


    }
}
