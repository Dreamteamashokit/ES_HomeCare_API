﻿using Dapper;
using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
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
    public class ClientData : IClientData
    {
        private IConfiguration configuration;
        public ClientData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> AddClient(ClientModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string _query = "INSERT INTO tblUser (UserKey,UserType,UserName,UserPassword,SSN,FirstName,MiddleName,LastName,DOB,Email,CellPhone,HomePhone,EmgPhone,EmgContact,Gender,MaritalStatus,Ethnicity,SupervisorId,IsActive,CreatedOn,CreatedBy) VALUES (@UserKey,@UserType,@UserName,@UserPassword,@SSN,@FirstName,@MiddleName,@LastName,@DOB,@Email,@CellPhone,@HomePhone,@EmgPhone,@EmgContact,@Gender,@MaritalStatus,@Ethnicity,@SupervisorId,@IsActive,@CreatedOn,@CreatedBy); select SCOPE_IDENTITY();";
                    _model.UserId = (int)(cnn.Query<int>(_query, _model).First());


                    string sqlQuery = "INSERT INTO tblClient (UserId,BillTo,Nurse,OfChild,AltId,ID2,ID3,InsuranceID,WorkerName,WorkerContact,ReferredBy,IsHourly,TimeSlip,PriorityCode,IsActive,CreatedOn,CreatedBy) VALUES (@UserId,@BillTo,@Nurse,@OfChild,@AltId,@ID2,@ID3,@InsuranceID,@WorkerName,@WorkerContact,@ReferredBy,@IsHourly,@TimeSlip,@PriorityCode,@IsActive,@CreatedOn,@CreatedBy)";


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

        public async Task<ServiceResponse<ClientModel>> GetClientDetail(int clientId)
        {
            ServiceResponse<ClientModel> obj = new ServiceResponse<ClientModel>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sqlqry = "Select y.*,x.AltId,x.ID2,x.ID3,x.InsuranceID,x.WorkerContact,x.WorkerName,x.ReferredBy,x.PriorityCode,x.TimeSlip,x.OfChild,x.Nurse,y.SupervisorId,(ISNULL(co.FirstName,'') + ' ' +   ISNULL(co.MiddleName,'') + ' ' + ISNULL(co.LastName,'') ) as CoordinatorName,(ISNULL(nu.FirstName,'') + ' ' +   ISNULL(nu.MiddleName,'') + ' ' + ISNULL(nu.LastName,'') ) NurseName ,ge.ItemName as GenderName,eth.ItemName as EthnicityName,ms.ItemName as MaritalStatusName from tblClient x inner join tblUser y on x.UserId=y.UserId Left join tblUser co on y.SupervisorId=co.UserId Left join tblUser nu on x.Nurse=nu.UserId Left join tblMaster ge on y.Gender=ge.MasterId Left join tblMaster eth on y.Ethnicity=eth.MasterId Left join tblMaster ms on y.MaritalStatus=ms.MasterId Where  x.UserId=@UserId;";

                var objResult = (await connection.QueryAsync<ClientModel>(sqlqry,
                       new { @UserId = clientId })).FirstOrDefault();

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<string>> SaveClientStatus(ClientStatus _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblClientSatus (ActivityID,StatusDate,ReferralCodeId," +
                        "note,clientId,OfficeUserId,ReferaalUserID,TextCheck,ScreenCheck,EmailCheck,Createdon,CreatedBy) " +
                        "Values(@ActivityID,@StatusDate,@ReferralCodeId,@note,@clientId,@OfficeUserId,@ReferaalUserID,@Text,@Screen," +
                        "@Email,@CreatedOn,@CreatedBy)";

                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        ActivityID = _model.ActivityId,
                        StatusDate = _model.Date,
                        ReferralCodeId = _model.ReferralCode,
                        note = _model.Note,
                        clientId = _model.ClientId,
                        OfficeUserId = _model.OfficeUserId,
                        ReferaalUserID = _model.OfficeUserReferralID,
                        Text = _model.Text,
                        Screen = _model.Screen,
                        Email = _model.Email,
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

        public async Task<ServiceResponse<IEnumerable<ClientStatusLst>>> GetClientStatusList(int ClientId)
        {
            ServiceResponse<IEnumerable<ClientStatusLst>> obj = new ServiceResponse<IEnumerable<ClientStatusLst>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select tm.ItemName as ActivityText,StatusDate as Date,tmm.ItemName as ReferralCodeText,note as Note from tblClientSatus tcs left join tblMaster tm on tcs.ActivityID=tm.ItemId " +
                              "left join tblMaster tmm on tcs.ReferralCodeId = tmm.ItemId where tm.MasterType = 6 and tmm.MasterType = 7 and tcs.clientId=@ClientId";
                IEnumerable<ClientStatusLst> cmeetings = (await connection.QueryAsync<ClientStatusLst>(sql, new { ClientId = ClientId }));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }

            return obj;
        }

        public async Task<ServiceResponse<List<Medicationcs>>> ClientMedicationcs(Medicationcs Model, int Flag)
        {
            ServiceResponse<List<Medicationcs>> obj = new ServiceResponse<List<Medicationcs>>();
            List<Medicationcs> clientsMedicationcs = new List<Medicationcs>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("Sp_SaveClientMedication", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@flag", Flag);
                    cmd.Parameters.AddWithValue("@MedicationID", Model.MedicationID);
                    cmd.Parameters.AddWithValue("@StartDate", Model.StartDate.ToString("dd-mm-yyyy") == "01-00-0001" ? DateTime.Now.Date : Model.StartDate.Date);
                    cmd.Parameters.AddWithValue("@EndDate", Model.EndDate.ToString("dd-mm-yyyy") == "01-00-0001" ? DateTime.Now.Date : Model.EndDate.Date);
                    cmd.Parameters.AddWithValue("@Medication", Model.MedicationText);
                    cmd.Parameters.AddWithValue("@NDC", Model.NDCText);
                    cmd.Parameters.AddWithValue("@Strength", Model.StrengthText);
                    cmd.Parameters.AddWithValue("@Dosage", Model.DosageText);
                    cmd.Parameters.AddWithValue("@Frequency", Model.FrequencyText);
                    cmd.Parameters.AddWithValue("@Route", Model.RouteText);
                    cmd.Parameters.AddWithValue("@Tabs", Model.TabsText);
                    cmd.Parameters.AddWithValue("@Prescriber", Model.PrescriberText);
                    cmd.Parameters.AddWithValue("@Classification", Model.ClassificationText);
                    cmd.Parameters.AddWithValue("@Instructions", Model.Instructionscheck);
                    cmd.Parameters.AddWithValue("@IsReminders", Model.Reminderscheck);
                    cmd.Parameters.AddWithValue("@IsInstructionscheck", Model.Instructionscheck);
                    cmd.Parameters.AddWithValue("@Isadministrationcheck", Model.administrationcheck);
                    cmd.Parameters.AddWithValue("@Isselfadministercheck", Model.selfadministercheck);
                    cmd.Parameters.AddWithValue("@UserId", Model.ClientID);
                    cmd.Parameters.AddWithValue("@CreatedOn", Model.createdOn.Date);
                    cmd.Parameters.AddWithValue("@CreatedBy", Model.CreatedBy);
                    cmd.Parameters.AddWithValue("@IsActive", Model.IsActive);
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            clientsMedicationcs.Add(new Medicationcs
                            {
                                MedicationID = Convert.ToInt32(table.Rows[i]["MedicationID"].ToString()),
                                StartDate = Convert.ToDateTime(table.Rows[i]["StartDate"]),
                                EndDate = Convert.ToDateTime(table.Rows[i]["EndDate"]),
                                MedicationText = string.IsNullOrEmpty(table.Rows[i]["Medication"].ToString()) ? table.Rows[i]["NDC"].ToString() : table.Rows[i]["Medication"].ToString(),
                                StrengthText = table.Rows[i]["Strength"].ToString(),
                                FrequencyText = table.Rows[i]["Frequency"].ToString(),
                                DosageText = table.Rows[i]["Dosage"].ToString(),
                                RouteText = string.IsNullOrEmpty(table.Rows[i]["Route"].ToString()) ? table.Rows[i]["Instructions"].ToString() : table.Rows[i]["Route"].ToString(),
                            });
                        }
                        obj.Result = true;
                    }
                    obj.Data = clientsMedicationcs;
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

        public async Task<ServiceResponse<string>> CreateServiceTask(IList<ServiceTaskModel> _list)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {

                    string sQuery = "INSERT INTO tblServiceTask (UserId,TaskId,Frequency,ServiceNote,IsActive,CreatedOn,CreatedBy) VALUES (@UserId,@TaskId,@Frequency,@ServiceNote,@IsActive,@CreatedOn,@CreatedBy);";


                    string Query = "MERGE INTO tblServiceTask AS TARGET USING( VALUES (@UserId, @TaskId, @Frequency, @ServiceNote, @IsActive, @CreatedOn, @CreatedBy) ) AS SOURCE(UserId, TaskId, Frequency, ServiceNote, IsActive, CreatedOn, CreatedBy) ON SOURCE.UserId = TARGET.UserId and SOURCE.TaskId = TARGET.TaskId WHEN MATCHED THEN UPDATE SET Frequency = @Frequency, ServiceNote = @ServiceNote,IsActive=@IsActive WHEN NOT MATCHED THEN INSERT(UserId, TaskId, Frequency, ServiceNote, IsActive, CreatedOn, CreatedBy) VALUES(SOURCE.UserId, SOURCE.TaskId, SOURCE.Frequency, SOURCE.ServiceNote, SOURCE.IsActive, SOURCE.CreatedOn, SOURCE.CreatedBy); ";



                    int rowsAffected = await cnn.ExecuteAsync(Query, _list.Select(_model =>
                    new
                    {
                        @UserId = _model.UserId,
                        @TaskId = _model.TaskId,
                        @Frequency = _model.Frequency,
                        @ServiceNote = _model.ServiceNote,
                        @IsActive = _model.IsActive,
                        @CreatedOn = _model.CreatedOn,
                        @CreatedBy = _model.CreatedBy,
                    }));

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

            return sres;
        }

        public async Task<ServiceResponse<IEnumerable<ServiceTaskView>>> GetServiceTaskList(int userId)
        {
            ServiceResponse<IEnumerable<ServiceTaskView>> obj = new ServiceResponse<IEnumerable<ServiceTaskView>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sqlqry = "Select x.TaskSrvId,y.TaskCode,y.TaskName,x.Frequency,x.ServiceNote,x.UserId from tblServiceTask x inner join tblTaskMaster y on x.TaskId=y.TaskId Where  x.UserId=@UserId and x.IsActive=1;";

                IEnumerable<ServiceTaskView> objResult = (await connection.QueryAsync<ServiceTaskView>(sqlqry, new { @UserId = userId }));

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<string>> DeleteService(int SrvId)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblServiceTask Set IsActive=@IsActive Where TaskSrvId=@SrvId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new { @IsActive = 0, @SrvId = SrvId });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Deleted Successfully";
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
        
        public async Task<ServiceResponse<string>> UpdateService(ServiceTaskModel item)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblServiceTask Set TaskId=@TaskId,Frequency=@Frequency,ServiceNote=@ServiceNote Where TaskSrvId=@TaskSrvId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new
                    {
                        @TaskSrvId = item.TaskSrvId,
                        @TaskId = item.TaskId,
                        @Frequency = item.Frequency,
                        @ServiceNote = item.ServiceNote,

                    });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Updated Successfully";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Updation Failed.";
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

        public async Task<ServiceResponse<string>> CreateEmpDeclined(EmployeeDecline _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string Query = "INSERT INTO tblEmpDeclined (ReportedDate,UserId,EmpId,CaseType,Reason,StartDate,Notes,IsActive,CreatedOn,CreatedBy) VALUES (@ReportedDate,@UserId,@EmpId,@CaseType,@Reason,@StartDate,@Notes,@IsActive,@CreatedOn,@CreatedBy);";

                    int rowsAffected = await connection.ExecuteAsync(Query, _model);

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

            return sres;
        }

        public async Task<ServiceResponse<IEnumerable<EmployeeDeclineView>>> GetEmpDeclined(int userId)
        {
            ServiceResponse<IEnumerable<EmployeeDeclineView>> obj = new ServiceResponse<IEnumerable<EmployeeDeclineView>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sqlqry = " Select x.*,(ISNULL(y.FirstName,'') + ' ' +   ISNULL(y.MiddleName,'') + ' ' + ISNULL(y.LastName,'') ) EmpName  from tblEmpDeclined x inner join tblUser y on x.EmpId=y.UserId Where  x.UserId=@UserId and x.IsActive=1;";

                IEnumerable<EmployeeDeclineView> objResult = (await connection.QueryAsync<EmployeeDeclineView>(sqlqry, new { @UserId = userId }));

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<string>> UpdateEmpDeclined(EmployeeDecline item)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblEmpDeclined Set ReportedDate=@ReportedDate,EmpId=@EmpId,StartDate=@StartDate,Reason=@Reason,Notes=@Notes Where DeclinedId=@DeclinedId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, item);

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Updated Successfully";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Updation Failed.";
                    }

                }
                return obj;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ClientEmrgencyInfo>>> ClienEmergencyInfo(ClientEmrgencyInfo Model)
        {
            ServiceResponse<IEnumerable<ClientEmrgencyInfo>> obj = new ServiceResponse<IEnumerable<ClientEmrgencyInfo>>();
            List<ClientEmrgencyInfo> clientsEmrgencyInfo = new List<ClientEmrgencyInfo>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("EmergencyInfoProc", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@type", Model.type);
                    cmd.Parameters.AddWithValue("@UserId", Model.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", Model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", Model.LastName);
                    cmd.Parameters.AddWithValue("@Relationship", Model.Relationship);
                    cmd.Parameters.AddWithValue("@Phone", Model.Phone);
                    cmd.Parameters.AddWithValue("@Email", Model.Email);
                    cmd.Parameters.AddWithValue("@Title", Model.Title);
                    cmd.Parameters.AddWithValue("@License", Model.License);
                    cmd.Parameters.AddWithValue("@LicenseEx", Model.LicenseExpires.Date);
                    cmd.Parameters.AddWithValue("@NPI", Model.NPINumber);
                    cmd.Parameters.AddWithValue("@Fax", Model.Fax);
                    cmd.Parameters.AddWithValue("@address", Model.Address);
                    cmd.Parameters.AddWithValue("@State", Model.State);
                    cmd.Parameters.AddWithValue("@city", Model.City);
                    cmd.Parameters.AddWithValue("@ZipCode", Model.Zip);
                    cmd.Parameters.AddWithValue("@IsActive", Model.IsActive);
                    cmd.Parameters.AddWithValue("@CreatedOn", Model.CreatedOn.Date);
                    cmd.Parameters.AddWithValue("@CreatedBy", Model.CreatedBy);
                    cmd.Parameters.AddWithValue("@ID", Model.Id);
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            clientsEmrgencyInfo.Add(new ClientEmrgencyInfo
                            {
                                UserId = Model.UserId,
                                type = Convert.ToInt32(table.Rows[i]["type"].ToString()) == 1 ? "Primary Contact" : "Emergency Contact",
                                Title = table.Rows[i]["Title"].ToString(),
                                FirstName = table.Rows[i]["FirstName"].ToString(),
                                LastName = table.Rows[i]["LastName"].ToString(),
                                License = table.Rows[i]["License"].ToString(),
                                LicenseExpires = string.IsNullOrEmpty(table.Rows[i]["LicenseExpires"].ToString()) ? DateTime.Now.Date : Convert.ToDateTime(table.Rows[i]["LicenseExpires"].ToString()),
                                NPINumber = table.Rows[i]["NPINumber"].ToString(),
                                Email = table.Rows[i]["Email"].ToString(),
                                Phone = table.Rows[i]["Phone"].ToString(),
                                Fax = table.Rows[i]["Fax"].ToString(),
                                Address = table.Rows[i]["Address"].ToString(),
                                State = table.Rows[i]["State"].ToString(),
                                City = table.Rows[i]["City"].ToString(),
                                Zip = table.Rows[i]["ZipCode"].ToString(),
                                IsActive = Convert.ToBoolean(table.Rows[i]["IsActive"]),
                                Id = Convert.ToInt16(table.Rows[i]["Id"]),
                                Edit = true
                            });
                        }
                        obj.Result = true;
                    }
                    obj.Data = clientsEmrgencyInfo;
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

        public async Task<ServiceResponse<string>> DeleteEmpDeclined(int declinedId)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblEmpDeclined Set IsActive=@IsActive Where DeclinedId=@DeclinedId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new { @IsActive = 0, @DeclinedId = declinedId });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Deleted Successfully";
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

        public async Task<ServiceResponse<string>> SaveClientContactLog(ClientContactLog _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblContactLog (UserId,OfficeUserId,EmpId,Reason,CallDateTime,ScheduleDate,FollowUpDate,Issue, " +
                        "ActionTaken,Notes,IsFollowUp,IsSchedule,IsActive,CreatedOn,CreatedBy) " +
                        "Values(@UserId,@OfficeUserId,@EmpId,@Reason,@CallDateTime,@ScheduleDate,@FollowUpDate,@Issue,@ActionTaken," +
                        "@Notes,@IsFollowUp,@IsSchedule,@IsActive,@CreatedOn,@CreatedBy)";

                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        UserId = _model.UserId,
                        OfficeUserId = _model.OfficeUserId,
                        EmpId = _model.EmpId,
                        Reason = _model.Reason,
                        CallDateTime = _model.CallDateTime,
                        ScheduleDate = _model.ScheduleDate,
                        FollowUpDate = _model.FollowUpDate,
                        Issue = _model.Issue,
                        ActionTaken = _model.ActionTaken,
                        Notes = _model.Notes,
                        IsFollowUp = _model.IsFollowUp,
                        IsSchedule = _model.IsSchedule,
                        IsActive = _model.IsActive,
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

        public async Task<ServiceResponse<IEnumerable<ClientContactLog>>> GetClientContactLogs(int ClientId)
        {
            ServiceResponse<IEnumerable<ClientContactLog>> obj = new ServiceResponse<IEnumerable<ClientContactLog>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select ContactlogId,UserId,OfficeUserId,EmpId,Reason, " +
                            " CallDateTime,ScheduleDate,FollowUpDate,Issue,ActionTaken,Notes,IsFollowUp,IsSchedule,  " +
                            " IsActive,CreatedOn,CreatedBy from tblContactLog(nolock) " +
                            " where IsActive=1 and  UserId = " + ClientId;

                IEnumerable<ClientContactLog> clogs = (await connection.QueryAsync<ClientContactLog>(sql, new { UserId = ClientId }));
                obj.Data = clogs;
                obj.Result = clogs.Any() ? true : false;
                obj.Message = clogs.Any() ? "Data Found." : "No Data found.";
            }

            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<ClientContactLog>>> getClientContactLogDetails(int contactLogId)
        {
            ServiceResponse<IEnumerable<ClientContactLog>> obj = new ServiceResponse<IEnumerable<ClientContactLog>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select ContactlogId,UserId,OfficeUserId,EmpId,Reason, " +
                            " CallDateTime,ScheduleDate,FollowUpDate,Issue,ActionTaken,Notes,IsFollowUp,IsSchedule,  " +
                            " IsActive,CreatedOn,CreatedBy from tblContactLog(nolock) " +
                            " where ContactlogId = " + contactLogId;

                IEnumerable<ClientContactLog> clogs = (await connection.QueryAsync<ClientContactLog>(sql, new { ContactlogId = contactLogId }));
                obj.Data = clogs;
                obj.Result = clogs.Any() ? true : false;
                obj.Message = clogs.Any() ? "Data Found." : "No Data found.";
            }

            return obj;
        }

        public async Task<ServiceResponse<string>> UpdateClientContactLog(ClientContactLog item)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "update tblContactLog set EmpId=@EmpId, Reason=@Reason, CallDateTime=@CallDateTime , " +
                          " ScheduleDate = @ScheduleDate,FollowUpDate = @FollowUpDate,Issue = @Issue , " +
                          " ActionTaken = @ActionTaken,Notes = @Notes,IsFollowUp = @IsFollowUp,IsSchedule = @IsSchedule " +
                          " where ContactlogId  =" + item.ContactlogId;

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new
                    {
                        @UserId = item.UserId,
                        @OfficeUserId = item.OfficeUserId,
                        @EmpId = item.EmpId,
                        @Reason = item.Reason,
                        @CallDateTime = item.CallDateTime,
                        @ScheduleDate = item.ScheduleDate,
                        @FollowUpDate = item.FollowUpDate,
                        @Issue = item.Issue,
                        @ActionTaken = item.ActionTaken,
                        @Notes = item.Notes,
                        @IsFollowUp = item.IsFollowUp,
                        @IsSchedule = item.IsSchedule
                    });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Updated Successfully";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Updation Failed.";
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

        public async Task<ServiceResponse<string>> DeleteClientContactLog(int contactLogId)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblContactLog Set IsActive=@IsActive Where ContactlogId=@ContactlogId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new { @IsActive = 0, @ContactlogId = contactLogId });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Deleted Successfully";
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


        public async Task<ServiceResponse<List<ClientNote>>> ClientNoteOperation(ClientNote Model, int Flag)
        {
            ServiceResponse<List<ClientNote>> obj = new ServiceResponse<List<ClientNote>>();
            List<ClientNote> clientsnotes = new List<ClientNote>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_ClientNotesOperation", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@flag", Flag);
                    cmd.Parameters.AddWithValue("@UserId", Model.UserId);
                    cmd.Parameters.AddWithValue("@NotesTypeId", Model.NotesTypeId);
                    cmd.Parameters.AddWithValue("@Notes", Model.Notes);
                    cmd.Parameters.AddWithValue("@OfficeUserId", Model.OfficeUserId);
                    cmd.Parameters.AddWithValue("@EmpId", Model.EmpId);
                    cmd.Parameters.AddWithValue("@NotifyTypeId", Model.NotifyTypeId);
                    cmd.Parameters.AddWithValue("@IsActive", Model.IsActive);
                    cmd.Parameters.AddWithValue("@CreatedOn", Model.CreatedOn);
                    cmd.Parameters.AddWithValue("@CreatedBy", Model.CreatedBy);                    
                    cmd.Parameters.AddWithValue("@NotesId", Model.NotesId);
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            clientsnotes.Add(new ClientNote
                            {
                                NotesId = Convert.ToInt32(table.Rows[i]["NotesId"].ToString()),
                                UserId = Convert.ToInt32(table.Rows[i]["UserId"]),
                                NotesTypeId = Convert.ToInt32(table.Rows[i]["NotesTypeId"]),
                                Notes = table.Rows[i]["Notes"].ToString(),
                                OfficeUserId = Convert.ToInt32(table.Rows[i]["OfficeUserId"].ToString()),
                                EmpId = Convert.ToInt32(table.Rows[i]["EmpId"].ToString()),
                                NotifyTypeId = Convert.ToInt16(table.Rows[i]["NotifyTypeId"].ToString()),
                                IsActive = Convert.ToInt16(table.Rows[i]["IsActive"].ToString()),
                                CreatedOn=Convert.ToDateTime(table.Rows[i]["CreatedOn"].ToString()),
                                CreatedBy=Convert.ToInt32(table.Rows[i]["CreatedBy"].ToString())
                            });
                        }
                        obj.Result = true;
                    }
                    obj.Data = clientsnotes;
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

        public async Task<ServiceResponse<string>> AddOtherInfo(OtherInfoModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "INSERT INTO tblOthers (UserId,CASA3,ContactId,InsuranceGrp,IsMedications,IsDialysis,IsOxygen,IsAids,IsCourtOrdered,FlowRate,ReunionLocations,ShelterName,TalCode,Shelter,Facility,Room,ServiceRequestDate,CareDate,DischargeDate,Notes,Allergies,CreatedBy,CreatedOn) VALUES (UserId,@CASA3,@ContactId,@InsuranceGrp,@IsMedications,@IsDialysis,@IsOxygen,@IsAids,@IsCourtOrdered,@FlowRate,@ReunionLocations,@ShelterName,@TalCode,@Shelter,@Facility,@Room,@ServiceRequestDate,@CareDate,@DischargeDate,@Notes,@Allergies,@CreatedBy,@CreatedOn); ";

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

        public async Task<ServiceResponse<string>> UpdateOtherInfo(OtherInfoModel item)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "UPDATE tblOthers SET CASA3=@CASA3,ContactId=@ContactId,InsuranceGrp=@InsuranceGrp,IsMedications=@IsMedications,IsDialysis=@IsDialysis,IsOxygen=@IsOxygen,IsAids=@IsAids,IsCourtOrdered=@IsCourtOrdered,FlowRate=@FlowRate,ReunionLocations=@ReunionLocations,ShelterName=@ShelterName,TalCode=@TalCode,Shelter=@Shelter,Facility=@Facility,Room=@Room,ServiceRequestDate=@ServiceRequestDate,CareDate=@CareDate,DischargeDate=@DischargeDate,Notes=@Notes,Allergies=@Allergies Where UserId=@UserId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, item);

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Updated Successfully";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Updation Failed.";
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

        public async Task<ServiceResponse<OtherInfoModel>> GetOtherInfo(int UserId)
        {
            ServiceResponse<OtherInfoModel> obj = new ServiceResponse<OtherInfoModel>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "Select CASA3, ContactId, InsuranceGrp, IsMedications, IsDialysis, IsOxygen, IsAids, IsCourtOrdered, FlowRate, ReunionLocations, ShelterName, TalCode, Shelter, Facility, Room, ServiceRequestDate, CareDate, DischargeDate, Notes, Allergies from tblOthers(nolock) Where UserId = @UserId";

                var objResult = (await connection.QueryAsync<OtherInfoModel>(sql, new { UserId = UserId })).FirstOrDefault();

                objResult.EntityId = objResult.OtherId;
                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }

            return obj;
        }

        public async Task<ServiceResponse<string>> AddDiagnosis(DiagnosisModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "INSERT INTO tblDiagnosis(UserId,DxId,OrderNo,IsPrimary,IsActive,CreatedBy,CreatedOn) VALUES(@UserId,@DxId,@OrderNo,@IsPrimary,@IsActive,@CreatedBy,@CreatedOn);";

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

        public async Task<ServiceResponse<string>> UpdateDiagnosis(DiagnosisModel item)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblDiagnosis Set DxId=@DxId,OrderNo=@OrderNo,IsPrimary=@IsPrimary Where DiagnosisId=@EntityId;";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, item);

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Updated Successfully";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Updation Failed.";
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

        public async Task<ServiceResponse<IEnumerable<DiagnosisView>>> GetDiagnosisModel(int UserId)
        {
            ServiceResponse<IEnumerable<DiagnosisView>> obj = new ServiceResponse<IEnumerable<DiagnosisView>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select x.DiagnosisId,x.DxId,x.OrderNo,x.IsPrimary,y.DxCodes,y.Description,(ISNULL(z.FirstName,'') + ' ' +   ISNULL(z.MiddleName,'') + ' ' + ISNULL(z.LastName,'') ) AddedBy, CONVERT(DATE, x.CreatedOn) CreatedOn from tblDiagnosis x Inner Join  tblDiagnosisMaster y on x.DxId=y.DxId Inner Join  tblUser z on x.CreatedBy=z.UserId Where x.IsActive=@IsActive and x.UserId=@UserId;";

                var objResult = (await connection.QueryAsync<DiagnosisView>(sql, new { IsActive = 1, UserId = UserId }));
                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }

            return obj;
        }

        public async Task<ServiceResponse<string>> DeleteDiagnosis(int DiagnosisId)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {

                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblDiagnosis Set IsActive=@IsActive Where DiagnosisId=@DiagnosisId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new { @IsActive = 0, @DiagnosisId = DiagnosisId });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Deleted Successfully";
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


    }


}
