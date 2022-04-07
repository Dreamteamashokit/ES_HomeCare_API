using Dapper;
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
                    string _query = "INSERT INTO tblUser (SSN,FirstName,MiddleName,LastName,DOB,Email,Gender,MaritalStatus," +
                        "EmgContact,UserName,UserPassword,IsActive,CreatedOn,CreatedBy) VALUES (@SSN,@FirstName,@MiddleName,@LastName,@DOB,@Email,@Gender,@MaritalStatus,@EmgContact," +
                        "@UserName,@UserPassword,@IsActive,@CreatedOn,@CreatedBy) select SCOPE_IDENTITY();";
                    _model.UserId = (int)(cnn.Query<int>(_query, _model).First());


                    string sqlQuery = "INSERT INTO tblClient (UserId,BillTo,FirstName,MiddleName,LastName,Email,Contact,EmgContact,Ethnicity,Gender,Coordinator,Nurse," +
                        "MaritalStatus,OfChild,SSN,DOB,AltId,ID2,ID3,InsuranceID,WorkerName,WorkerContact,ReferredBy,IsHourly,TimeSlip,PriorityCode," +
                        "IsActive,CreatedOn,CreatedBy) VALUES (@UserId,@BillTo,@FirstName,@MiddleName,@LastName,@Email,@Contact,@EmgContact," +
                        "@Ethnicity,@Gender,@Coordinator,@Nurse,@MaritalStatus,@OfChild,@SSN,@DOB,@AltId,@ID2,@ID3,@InsuranceID,@WorkerName," +
                        "@WorkerContact,@ReferredBy,@IsHourly,@TimeSlip,@PriorityCode,@IsActive,@CreatedOn,@CreatedBy)";


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
                string sql = "select y.*,x.AltId,x.ID2,x.ID3,x.InsuranceID,x.WorkerContact,x.WorkerName,x.ReferredBy,x.PriorityCode," +
                    "x.TimeSlip,x.OfChild from tblClient x inner join tblUser y on x.UserId=y.UserId where x.UserId=@UserId; ";

                var objResult = (await connection.QueryAsync<ClientModel>(sql,
                       new { @UserId = clientId })).FirstOrDefault();

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }


        public async Task<ServiceResponse<string>> savenewclient(Client client)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_SAVE_CLIENT", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BillTo", client.BillTo);
                    cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", client.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", client.LastName);
                    cmd.Parameters.AddWithValue("@CellPhone", client.CellPhone);
                    cmd.Parameters.AddWithValue("@Ethnicity", client.Ethnicity);
                    cmd.Parameters.AddWithValue("@Email", client.Email);
                    cmd.Parameters.AddWithValue("@InsurenceId", client.InsurenceId);
                    cmd.Parameters.AddWithValue("@OfChild", client.OfChild);
                    cmd.Parameters.AddWithValue("@SSN", client.SSN);
                    cmd.Parameters.AddWithValue("@ExtClientId", client.ExtClientId);
                    cmd.Parameters.AddWithValue("@Gender", client.Gender);
                    cmd.Parameters.AddWithValue("@Nurse", client.Nurse);
                    cmd.Parameters.AddWithValue("@ClassCordinator", client.ClassCordinator);
                    cmd.Parameters.AddWithValue("@MaritalStatus", client.MaritalStatus);
                    cmd.Parameters.AddWithValue("@City", client.City);
                    cmd.Parameters.AddWithValue("@Country", client.Country);
                    cmd.Parameters.AddWithValue("@CState", client.CState);
                    cmd.Parameters.AddWithValue("@ZipCode", client.ZipCode);
                    cmd.Parameters.AddWithValue("@ReferredBy", client.ReferredBy);
                    cmd.Parameters.AddWithValue("@EmgContact", client.EmgContact);
                    cmd.Parameters.AddWithValue("@CaseWorkerPhone", client.CaseWorkerPhone);
                    cmd.Parameters.AddWithValue("@CaseWorkerEmail", client.CaseWorkerEmail);
                    cmd.Parameters.AddWithValue("@IsActive", client.IsActive);

                    cmd.Parameters.AddWithValue("@EmgPhone", client.EmgPhone);
                    cmd.Parameters.AddWithValue("@EmgEmail", client.EmgEmail);


                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        sres.Result = true;
                        sres.Data = "New Client Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new client creation.";
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

        public async Task<ServiceResponse<List<Client>>> GetClientList()
        {
            ServiceResponse<List<Client>> obj = new ServiceResponse<List<Client>>();
            List<Client> clients = new List<Client>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_CLIENTS", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            clients.Add(new Client
                            {
                                ClientId = Convert.ToInt32(table.Rows[i]["ClientId"].ToString()),
                                ClientName = table.Rows[i]["ClientName"].ToString(),
                                ExtClientId = table.Rows[i]["ExtClientId"].ToString()
                            });
                        }
                        obj.Result = true;
                    }
                    obj.Data = clients;
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

        public async Task<ServiceResponse<IEnumerable<ClientMeetings>>> GetClientMeetings(string startdate, string cID)
        {
            ServiceResponse<IEnumerable<ClientMeetings>> obj = new ServiceResponse<IEnumerable<ClientMeetings>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    connection.Open();
                    IEnumerable<ClientMeetings> cmeetings = (await connection.QueryAsync<MeetingDBClass>("SP_GET_ClientMeetings",
                            new { @StartDate = startdate, @ClientID = cID },
                            commandType: CommandType.StoredProcedure))
                            .GroupBy(x => new
                            {
                                x.ClientId,
                                x.CellPhone,
                                x.City,
                                x.ClassCordinator,
                                x.Country,
                                x.CState,
                                x.Email,
                                x.ExtClientId,
                                x.FirstName,
                                x.LastName,
                                x.ZipCode,
                                x.MiddleName,
                            })
                            .Select(y => new ClientMeetings()
                            {
                                CellPhone = y.Key.CellPhone,
                                City = y.Key.City,
                                ClassCordinator = y.Key.ClassCordinator,
                                ZipCode = y.Key.ZipCode,
                                LastName = y.Key.LastName,
                                ClientId = y.Key.ClientId,
                                Country = y.Key.Country,
                                CState = y.Key.CState,
                                Email = y.Key.Email,
                                ExtClientId = y.Key.ExtClientId,
                                FirstName = y.Key.FirstName,
                                MiddleName = y.Key.MiddleName,
                                MeetingInfo = y.Where(z => z.MeetingId != null).Select(
                                    u => new MeetingInfo()
                                    {
                                        MeetingId = u.MeetingId,
                                        EndTime = u.EndTime,
                                        MClientId = u.MClientId,
                                        MeetingDate = u.MeetingDate.ToString("MM/dd/yyyy"),
                                        MeetingNote = u.MeetingNote,
                                        MeetingStartDate = u.MeetingStartDate.ToString("MM/dd/yyyy"),
                                        StartTime = u.StartTime,
                                        TotalMeetingHrs = u.TotalMeetingHrs,
                                        TotalMeetingMins = u.TotalMeetingMins
                                    }
                                    ).OrderBy(m => m.MClientId).ThenBy(m => m.MeetingStartDate).ThenBy(m => m.MeetingOrder).ToList()
                            }
                        );
                    connection.Close();
                    connection.Dispose();
                    obj.Data = cmeetings;
                    obj.Result = cmeetings.Any() ? true : false;
                    obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
                }
                return obj;
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

        public async Task<ServiceResponse<string>> scheduleclientmeeting(MeetingDetails meeting)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_SCHEDULE_CLIENT_MEETING", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MeetingNote", meeting.MeetingNote);
                    cmd.Parameters.AddWithValue("@MeetingDate", meeting.MeetingDate);
                    cmd.Parameters.AddWithValue("@MeetingStartHrsTime", meeting.MeetingStartHrsTime);
                    cmd.Parameters.AddWithValue("@MeetingStartMinsTime", meeting.MeetingStartMinsTime);
                    cmd.Parameters.AddWithValue("@MeetingEndHrsTime", meeting.MeetingEndHrsTime);
                    cmd.Parameters.AddWithValue("@MeetingEndMinsTime", meeting.MeetingEndMinsTime);
                    cmd.Parameters.AddWithValue("@TotalMeetingHrs", meeting.TotalMeetingHrs);
                    cmd.Parameters.AddWithValue("@TotalMeetingMins", meeting.TotalMeetingMins);
                    cmd.Parameters.AddWithValue("@ClientID", meeting.MClientId);
                    cmd.Parameters.AddWithValue("@StartTimeType", meeting.StartTimeType);
                    cmd.Parameters.AddWithValue("@EndTimeType", meeting.EndTimeType);

                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        sres.Result = true;
                        sres.Data = "New Client Meeting Scheduled.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed to schedule client meeting.";
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
                        clientId = _model.clientId,
                        OfficeUserId = _model.OfficeUserId,
                        ReferaalUserID = _model.OfficeUserReferralID,
                        Text = _model.text,
                        Screen = _model.screen,
                        Email = _model.email,                       
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



    }
}
