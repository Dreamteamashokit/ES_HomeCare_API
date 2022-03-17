using Dapper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
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

        public async Task<ServiceResponse<string>> savenewemployee(Employee client)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_SAVE_Employee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", client.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", client.LastName);
                    cmd.Parameters.AddWithValue("@CellPhone", client.CellPhone);
                    cmd.Parameters.AddWithValue("@HomePhone", client.HomePhone);
                    cmd.Parameters.AddWithValue("@Email", client.Email);
                    cmd.Parameters.AddWithValue("@DateOfHire", client.DateOfHire);
                    cmd.Parameters.AddWithValue("@DateOfFirstCase", client.DateOfFirstCase);
                    cmd.Parameters.AddWithValue("@DOB", client.DOB);
                    cmd.Parameters.AddWithValue("@SSN", client.SSN);
                    cmd.Parameters.AddWithValue("@ExtEmpId", client.ExtEmpId);
                    cmd.Parameters.AddWithValue("@Gender", client.Gender);
                    cmd.Parameters.AddWithValue("@HRSupervisor", client.HrSupervisor);
                    cmd.Parameters.AddWithValue("@Enthnicity", client.Enthnicity);
                    cmd.Parameters.AddWithValue("@MaritalStatus", client.MaritalStatus);
                    cmd.Parameters.AddWithValue("@City", client.City);
                    cmd.Parameters.AddWithValue("@Country", client.Country);
                    cmd.Parameters.AddWithValue("@CState", client.CState);
                    cmd.Parameters.AddWithValue("@ZipCode", client.ZipCode);
                    cmd.Parameters.AddWithValue("@EmgContact", client.EmgContact);
                    cmd.Parameters.AddWithValue("@EmgPhone", client.EmgPhone);
                    cmd.Parameters.AddWithValue("@IsActive", client.IsActive);

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

        public async Task<ServiceResponse<List<Employee>>> GetEmployeesList()
        {
            ServiceResponse<List<Employee>> obj = new ServiceResponse<List<Employee>>();
            List<Employee> emp = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_EMPLOYEES", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            emp.Add(new Employee
                            {
                                EmpID = Convert.ToInt32(table.Rows[i]["EmpId"].ToString()),
                                EmpName = table.Rows[i]["EmpName"].ToString(),
                                CellPhone = table.Rows[i]["CellPhone"].ToString(),
                                HasEmail = table.Rows[i]["HasEmail"].ToString(),
                                HasDOB = table.Rows[i]["HasDOB"].ToString(),
                                IsActive = Convert.ToInt32(table.Rows[i]["IsActive"].ToString()),
                                //EnteredDate = table.Rows[i]["CreatedOn"].ToString(),
                                CState = table.Rows[i]["CState"].ToString()
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

        public async Task<ServiceResponse<string>> DeleteEmployee(int EmpId)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_DELETE_EMPLOYEE", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EMPID", EmpId);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Employee Deleted";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Deletion Failed.";
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

        public async Task<ServiceResponse<Employee>> GetEmployeeById(string EmpId)
        {
            ServiceResponse<Employee> obj = new ServiceResponse<Employee>();

            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "SELECT * FROM tblEmployee Where EmpId=@EmpId;";
                IEnumerable<Employee> cmeetings = (await connection.QueryAsync<Employee>(sql,
                         new { @EmpId = EmpId }));
                obj.Data = cmeetings.FirstOrDefault();
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;









            //Employee emp = new Employee();
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            //    {
            //        SqlCommand cmd = new SqlCommand("SP_GET_EmployeeInfoByID", con);
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@EID", EmpId);
            //        DataTable table = new DataTable();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(table);
            //        if (table.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < table.Rows.Count; i++)
            //            {
            //                emp.EmpID = Convert.ToInt32(table.Rows[i]["EmpId"].ToString());
            //                emp.FirstName = table.Rows[i]["FirstName"].ToString();
            //                emp.LastName = table.Rows[i]["LastName"].ToString();
            //                emp.MiddleName = table.Rows[i]["MiddleName"].ToString();
            //                emp.CellPhone = table.Rows[i]["CellPhone"].ToString();
            //                emp.Email = table.Rows[i]["Email"].ToString();
            //                emp.DOB = table.Rows[i]["DOB"].ToString();
            //                emp.HomePhone = table.Rows[i]["HomePhone"].ToString();
            //                emp.ExtEmpId = table.Rows[i]["ExtEmpId"].ToString();
            //                emp.Country = table.Rows[i]["Country"].ToString();
            //                emp.City = table.Rows[i]["City"].ToString();
            //                emp.CState = table.Rows[i]["CState"].ToString();
            //                emp.ZipCode = table.Rows[i]["ZipCode"].ToString();
            //                emp.Gender = table.Rows[i]["Gender"].ToString();
            //            }
            //            obj.Result = true;
            //        }
            //        obj.Data = emp;
            //        return obj;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    obj.Message = ex.Message;
            //    return obj;
            //}
            //finally
            //{

            //}





















        }



        #region Address

        public async Task<ServiceResponse<string>> AddEmpAddress(AddressModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblAddress (EmpId,AddressType,Owner,FlatNo,Address,City,Country,State,ZipCode,CreatedOn,CreatedBy) Values (@EmpId,@AddressType,@Owner,@FlatNo,@Address,@City,@Country,@State,@ZipCode,@CreatedOn,@CreatedBy);";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        EmpId = _model.EmpId,
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
                string sql = "SELECT * FROM tblAddress Where EmpId=@EmpId;";
                IEnumerable<AddressModel> cmeetings = (await connection.QueryAsync<AddressModel>(sql,
                         new { @EmpId = empId }));
                obj.Data = cmeetings.FirstOrDefault();
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        #endregion



        public async Task<ServiceResponse<string>> AddIncident(IncidentMode _model)
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

        public async Task<ServiceResponse<IEnumerable<IncidentMode>>> GetIncidentList(int empId)
        {
            ServiceResponse<IEnumerable<IncidentMode>> obj = new ServiceResponse<IEnumerable<IncidentMode>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sql = "SELECT * FROM tblIncident Where EmpId=@EmpId;";

                IEnumerable<IncidentMode> cmeetings = (await connection.QueryAsync<IncidentMode>(sql,
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
                    string sqlQuery = "Insert Into tblAttendance (EmpId,Reason,StartDate,EndDate,Notes,CreatedOn,CreatedBy) Values (@EmpId,@Reason,@StartDate,@EndDate,@Notes,@CreatedOn,@CreatedBy);";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        EmpId = _model.EmpId,
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
                        "Values(@EmpId,@TypeStatusID,@Scheduling,@OfficeUserId,@Note,@Resume,@Rehire,@Text,@Screen," +
                        "@Email,@EffectiveDate,@ReturnDate,@CreatedOn,@CreatedBy)";

                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        EmpId = _model.EmployeeId,
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


        public async Task<ServiceResponse<IEnumerable<AvailabilityStatus>>> GetEmpStatusList()
        {
            ServiceResponse<IEnumerable<AvailabilityStatus>> obj = new ServiceResponse<IEnumerable<AvailabilityStatus>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select Text as StatusType,EffectiveDate,ReturnDate,OKResume as Resume,ReHire as Rehire,Note " +
                    "from dbo.tblEmpStatus ES inner join tblEmpStatusMaster ESM  on ES.TypeId = ESM.Id; ";
                IEnumerable<AvailabilityStatus> cmeetings = (await connection.QueryAsync<AvailabilityStatus>(sql));
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
                    string sqlQuery = "Insert Into tblCompliance (EmpId,DueDate,CompletedOn,Category,Code,Result,Nurse,Notes,CreatedOn,CreatedBy) Values (@EmpId,@DueDate,@CompletedOn,@Category,@Code,@Result,@Nurse,@Notes,@CreatedOn,@CreatedBy);";

                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        EmpId = _model.EmpId,
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


        public async Task<ServiceResponse<string>> SaveEmpPayRate(SaveEmployeeRate client)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {

                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {

                    SqlCommand cmd = new SqlCommand("SP_SaveEmpPayRateProc", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", client.EmpId);
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
                    cmd.Parameters.AddWithValue("@EmpId", client.EmpId);

                    cmd.Parameters.AddWithValue("@ReportedDate", client.RepotedDate);
                    cmd.Parameters.AddWithValue("@ClientID", client.clientId);
                    cmd.Parameters.AddWithValue("@caseid", client.Casetypeid);
                    cmd.Parameters.AddWithValue("@DeclinedReason", client.DeclineReason);
                    cmd.Parameters.AddWithValue("@AssignmentStartDate", client.AssignmentStart);
                    cmd.Parameters.AddWithValue("@Note", client.Note);
                    cmd.Parameters.AddWithValue("@Day", client.Day);
                    cmd.Parameters.AddWithValue("@Week", client.week);
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
                                week = Convert.ToInt16(table.Rows[i]["Week"].ToString()),
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
