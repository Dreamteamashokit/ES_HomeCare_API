
using Dapper;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.Model.Meeting;
using ES_HomeCare_API.WebAPI.Data.IData;
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
    public class MeetingData: IMeetingData
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


    }
}
