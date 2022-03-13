using Dapper;
using ES_HomeCare_API.Model;
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
    
    
    
    
    
    
    
    }
}
