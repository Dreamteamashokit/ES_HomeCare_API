using Dapper;
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
                    cmd.Parameters.AddWithValue("@StartDate",Model.StartDate.ToString("dd-mm-yyyy")=="01-00-0001"?DateTime.Now.Date: Model.StartDate.Date);
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
                    cmd.Parameters.AddWithValue("@CreatedOn",Model.createdOn.Date);
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
                                StartDate =Convert.ToDateTime(table.Rows[i]["StartDate"]),
                                EndDate = Convert.ToDateTime(table.Rows[i]["EndDate"]),
                                MedicationText = string.IsNullOrEmpty(table.Rows[i]["Medication"].ToString()) ? table.Rows[i]["NDC"].ToString() : table.Rows[i]["Medication"].ToString(),
                                StrengthText = table.Rows[i]["Strength"].ToString(),
                                FrequencyText = table.Rows[i]["Frequency"].ToString(),
                                DosageText = table.Rows[i]["Dosage"].ToString(),
                                RouteText = string.IsNullOrEmpty(table.Rows[i]["Route"].ToString()) ? table.Rows[i]["Instructions"].ToString() : table.Rows[i]["Route"].ToString(),
                            }) ;
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
                    string Query = "INSERT INTO tblServiceTask (UserId,TaskId,Frequency,ServiceNote,IsActive,CreatedOn,CreatedBy) VALUES (@UserId,@TaskId,@Frequency,@ServiceNote,@IsActive,@CreatedOn,@CreatedBy);";

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

                string sqlqry = "Select x.TaskSrvId,y.TaskCode,y.TaskName,x.Frequency,x.ServiceNote,x.UserId from tblServiceTask x inner join tblTaskMaster y on x.TaskId=y.TaskId Where  x.UserId=@UserId;";

                IEnumerable<ServiceTaskView> objResult = (await connection.QueryAsync<ServiceTaskView>(sqlqry,new { @UserId = userId }));

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }
    }
}