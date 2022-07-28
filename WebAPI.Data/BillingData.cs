using Dapper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Billing;
using ES_HomeCare_API.ViewModel.Invoice;
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
    public class BillingData : IBillingData
    {

        private IConfiguration configuration;
        public BillingData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> AddPayer(PayerModel _model)
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



                    string sqlQuery = @"INSERT INTO tblPayer
(PayerName,BillToName,Email,Phone,Fax,NPI,FedId,ETIN,Taxonomy,MedicaidId,IsActive,CreatedOn,CreatedBy)
VALUES
(@PayerName,@BillToName,@Email,@Phone,@Fax,@NPI,@FedId,@ETIN,@Taxonomy,@MedicaidId,@IsActive,@CreatedOn,@CreatedBy)";


                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = rowsAffected.ToString();
                    }
                    else
                    {
                        sres.Data = rowsAffected.ToString();
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

        public async Task<ServiceResponse<string>> UpdatePayer(PayerModel _model)
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



                    string sqlQuery = @"UPDATE tblPayer SET PayerName=@PayerName,BillToName=@BillToName,Email=@Email,Phone=@Phone,Fax=@Fax,NPI=@NPI,FedId=@FedId,ETIN=@ETIN,
Taxonomy=@Taxonomy,MedicaidId=@MedicaidId,IsActive=@IsActive,CreatedOn=@CreatedOn,CreatedBy=@CreatedBy
WHERE PayerId=@PayerId";


                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = rowsAffected.ToString();
                    }
                    else
                    {
                        sres.Data = rowsAffected.ToString();
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

        public async Task<ServiceResponse<IEnumerable<PayerModel>>> GetPayerList()
        {
            ServiceResponse<IEnumerable<PayerModel>> obj = new ServiceResponse<IEnumerable<PayerModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"Select * from tblPayer Where IsActive = @IsActive";
                IEnumerable<PayerModel> dataResult = (await connection.QueryAsync<PayerModel>(sql,
                         new { @IsActive = (int)Status.Active }));
                obj.Data = dataResult;
                obj.Result = dataResult.Any() ? true : false;
                obj.Message = dataResult.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        public async Task<ServiceResponse<string>> DelPayer(int PayerId)
        {
            ServiceResponse<string> result = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlQuery = "Update tblPayer Set IsActive=@IsActive where PayerId=@PayerId;";
                    var modeMapping = new
                    {
                        @PayerId = PayerId,
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
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Data = null;
                result.Message = ex.Message;
            }
            return result;
        }




        public async Task<ServiceResponse<BillingSummaryInfoModel>> GetBillingSummaryInfo(int userId)
        {
            ServiceResponse<BillingSummaryInfoModel> obj = new ServiceResponse<BillingSummaryInfoModel>();
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                SqlCommand cmd = new SqlCommand("GetBillingSummaryInfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        obj.Data = new BillingSummaryInfoModel
                        {
                            BillingId = Convert.ToInt32(table.Rows[i]["BillingId"]),
                            PayerId = Convert.ToInt32(table.Rows[i]["PayerId"]),
                            Type = table.Rows[i]["Type"] == null ? "" : table.Rows[i]["Type"].ToString(),
                            DueDate = string.IsNullOrWhiteSpace(table.Rows[i]["DueDate"].ToString()) ? "" : table.Rows[i]["DueDate"].ToString(),
                            BillToName = string.IsNullOrWhiteSpace(table.Rows[i]["BillToName"].ToString()) ? "" : table.Rows[i]["BillToName"].ToString()
                        };
                        obj.Result = true;
                        obj.Message = "Billing details retrive successfull.";
                    }
                }
                else
                {
                    obj.Result = true;
                    obj.Message = "Billing details does not exists.";
                }
                return obj;
            }
        }


        public async Task<ServiceResponse<IEnumerable<BillingStatusViewModel>>> GetBillingStatusList()
        {
            ServiceResponse<IEnumerable<BillingStatusViewModel>> obj = new ServiceResponse<IEnumerable<BillingStatusViewModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"SELECT TBS.BillingStatusId,TBS.[Name] FROM tblBillingStatus TBS WHERE TBS.ISACTIVE = @IsActive";
                IEnumerable<BillingStatusViewModel> dataResult = (await connection.QueryAsync<BillingStatusViewModel>(sql, new { @IsActive = true }));
                obj.Data = dataResult;
                obj.Result = dataResult.Any() ? true : false;
                obj.Message = dataResult.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }


        public async Task<ServiceResponse<IEnumerable<PayrollStatusViewModel>>> GetPayrollStatusList()
        {
            ServiceResponse<IEnumerable<PayrollStatusViewModel>> obj = new ServiceResponse<IEnumerable<PayrollStatusViewModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"SELECT TPS.PayRollStatusId,TPS.[Name] FROM tblPayrollStatus TPS WHERE TPS.ISACTIVE = @IsActive";
                IEnumerable<PayrollStatusViewModel> dataResult = (await connection.QueryAsync<PayrollStatusViewModel>(sql, new { @IsActive = true }));
                obj.Data = dataResult;
                obj.Result = dataResult.Any() ? true : false;
                obj.Message = dataResult.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }




        public async Task<ServiceResponse<IEnumerable<ClientSchedule>>> GetScheduleBilling()
        {
            ServiceResponse<IEnumerable<ClientSchedule>> obj = new ServiceResponse<IEnumerable<ClientSchedule>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"Select y.MeetingRateId as ScheduleRateId,z.BillTo as PayerId, xz.PayerName, x.MeetingId, x.ClientId,
xx.FirstName +' '+ ISNULL(xx.MiddleName,'')+' ' + xx.LastName as ClientName,
x.EmpId,xy.FirstName +' '+ ISNULL(xy.MiddleName,'')+' ' + xy.LastName as EmpName,
x.MeetingDate,x.IsCompleted as ScheduleStatus,y.BillingCode,y.BillingRate,y.BillingUnits,y.BillingTotal,y.BillingStatus,
y.PayrollPayStatus as PayrollStatus from tblMeeting x inner join tblMeetingRate y on x.MeetingId=y.MeetingId
inner join tblUser xx on x.ClientId= xx.UserId
inner join tblUser xy on x.EmpId= xy.UserId
inner Join tblClient z on x.ClientId= z.UserId
inner join tblPayer xz on z.BillTo= xz.PayerId";
                IEnumerable<ScheduleBillingModel> ObjData = (await connection.QueryAsync<ScheduleBillingModel>(sql));

                IEnumerable<ClientSchedule> result = ObjData.GroupBy(x => x.ClientId).Select(y => new ClientSchedule
                {

                    ClientId = y.Key,
                    ClientName = y.FirstOrDefault().ClientName,
                    PayerId = y.FirstOrDefault().PayerId,
                    PayerName = y.FirstOrDefault().PayerName,
                    Appointments = y.Count(),
                    Units = y.Sum(z => z.BillingUnits),
                    Amounts = y.Sum(z => z.BillingTotal),
                    ConfirmList = y.Where(z => z.BillingStatus == (short)BillingStatus.Confirmed).ToList(),
                    HoldList = y.Where(z => z.BillingStatus == (short)BillingStatus.Hold).ToList(),
                    UnconfirmList = y.Where(z => z.BillingStatus == (short)BillingStatus.Nonbillable).ToList(),

                });




                obj.Data = result;
                obj.Result = result.Any() ? true : false;
                obj.Message = result.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<ClientSchedule>>> GetScheduleBilling(SearchSchedule model)
        {
            ServiceResponse<IEnumerable<ClientSchedule>> obj = new ServiceResponse<IEnumerable<ClientSchedule>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"Select y.MeetingRateId as ScheduleRateId,z.BillTo as PayerId, xz.PayerName, x.MeetingId, x.ClientId,
xx.FirstName +' '+ ISNULL(xx.MiddleName,'')+' ' + xx.LastName as ClientName,
x.EmpId,xy.FirstName +' '+ ISNULL(xy.MiddleName,'')+' ' + xy.LastName as EmpName,
x.MeetingDate,x.IsCompleted as ScheduleStatus,y.BillingCode,y.BillingRate,y.BillingUnits,y.BillingTotal,y.BillingStatus,
y.PayrollPayStatus as PayrollStatus from tblMeeting x inner join tblMeetingRate y on x.MeetingId=y.MeetingId
inner join tblUser xx on x.ClientId= xx.UserId
inner join tblUser xy on x.EmpId= xy.UserId
inner Join tblClient z on x.ClientId= z.UserId
inner join tblPayer xz on z.BillTo= xz.PayerId
Where (z.BillTo=(CASE WHEN (@PayerId IS NULL OR @PayerId=0) THEN z.BillTo ELSE @PayerId  END) And
x.EmpId=(CASE WHEN (@EmpId IS NULL OR @EmpId=0)  THEN x.EmpId ELSE @EmpId END) And
x.ClientId=(CASE WHEN (@ClientId IS NULL OR @ClientId=0) THEN x.ClientId ELSE @ClientId END)) AND  
CAST(x.MeetingDate AS DATE) Between CAST(@FromDate AS DATE)  And CAST(@ToDate AS DATE)";
                IEnumerable<ScheduleBillingModel> ObjData = (await connection.QueryAsync<ScheduleBillingModel>(sql, new
                {
                    @FromDate = model.FromDate,
                    @ToDate = model.ToDate,
                    @PayerId = model.PayerId,
                    @EmpId = model.EmpId,
                    @ClientId = model.ClientId,
                })); ;

                IEnumerable<ClientSchedule> result = ObjData.GroupBy(x => x.ClientId).Select(y => new ClientSchedule
                {
                    ClientId = y.Key,
                    ClientName = y.FirstOrDefault().ClientName,
                    PayerId = y.FirstOrDefault().PayerId,
                    PayerName = y.FirstOrDefault().PayerName,
                    Appointments = y.Count(),
                    Units = y.Sum(z => z.BillingUnits),
                    Amounts = y.Sum(z => z.BillingTotal),
                    ConfirmList = y.Where(z => z.BillingStatus == (short)BillingStatus.Confirmed).ToList(),
                    HoldList = y.Where(z => z.BillingStatus == (short)BillingStatus.Hold).ToList(),
                    UnconfirmList = y.Where(z => z.BillingStatus == (short)BillingStatus.Nonbillable).ToList(),

                });




                obj.Data = result;
                obj.Result = result.Any() ? true : false;
                obj.Message = result.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }




        public async Task<ServiceResponse<BillingPayerRateViewModel>> GetBillingPayerRate(long payerId, long clientId, long meetingId)
        {
            ServiceResponse<BillingPayerRateViewModel> obj = new ServiceResponse<BillingPayerRateViewModel>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                //string sql = @"SELECT DISTINCT TOP 1 TB.BillingId,TB.PayerId,TP.PayerName,TB.ClientId,TPR.RateId,TPR.BillCode,TPR.TaxRate,
                //             TPR.Type,TPR.Unit,TPR.ValidFrom,TPR.ValidTo,
                //             (DATEDIFF(MINUTE, TM.StartTime,TM.EndTime)/15) AS CalculateUnit,(TPR.TaxRate * (DATEDIFF(MINUTE, TM.StartTime,TM.EndTime)/15)) AS BillTotal,
                //             (CASE WHEN TM.IsCompleted = 1 THEN 'Confirmed' ELSE 'Nonbillable' END) AS BillingStatus
                //             FROM tblBilling TB
                //             INNER JOIN tblPayer TP ON TB.PayerId = TP.Payerid
                //             INNER JOIN tblPayerrate TPR ON TB.PayerId = TPR.Payerid
                //             INNER JOIN tblMeeting TM ON TB.ClientId = TM.ClientId
                //             WHERE TB.IsActive = 1 AND TB.ClientId = @clientId AND TM.MeetingId = @meetingId AND TPR.IsActive = 1 
                //             AND TM.IsStatus = 1 AND TP.PayerId = 2 
                //             AND TM.MeetingDate BETWEEN TPR.ValidFrom AND TPR.ValidTo ORDER BY TPR.RateId DESC";

                String sql = @"SELECT DISTINCT TOP 1 TB.BillingId,TB.PayerId,TP.PayerName,TB.ClientId,TM.MeetingId,TPR.RateId,
                                TPR.BillCode,TPR.TaxRate,TER.RateId as EmpRateId,TPR.Type,TPR.Unit,TPR.ValidFrom,TPR.ValidTo,
                                TER.EmpId,TER.Hourly AS PayRate,TM.MeetingDate
                                FROM tblBilling TB
                                INNER JOIN tblPayer TP ON TB.PayerId = TP.Payerid
                                INNER JOIN tblPayerrate TPR ON TB.PayerId = TPR.Payerid
                                INNER JOIN tblMeeting TM ON TB.ClientId = TM.ClientId
                                LEFT JOIN tblEmpRate TER ON TER.EmpId = TM.EmpId
                                WHERE TB.IsActive = 1 AND TB.ClientId = @clientId AND TM.MeetingId = @meetingId AND TPR.IsActive = 1 
                                AND TM.IsStatus = 1 AND TP.PayerId = @payerId AND TM.MeetingDate BETWEEN TPR.ValidFrom AND TPR.ValidTo 
                                ORDER BY TPR.RateId DESC";

                BillingPayerRateViewModel result = (await connection.QueryAsync<BillingPayerRateViewModel>(sql, new
                {
                    @clientId = clientId,
                    @payerId = payerId,
                    @meetingId = meetingId
                })).FirstOrDefault();

                obj.Data = result;
                obj.Result = result != null ? true : false;
                obj.Message = result != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<IList<PayerListViewModel>>> GetPayerListByclientIdAndmeetingId(long clientId, long meetingId)
        {
            ServiceResponse<IList<PayerListViewModel>> obj = new ServiceResponse<IList<PayerListViewModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"SELECT DISTINCT TP.PayerId,TP.PayerName
                                FROM tblPayer TP
                                INNER JOIN tblBilling TB ON TB.PayerId = TP.Payerid
                                LEFT JOIN tblPayerrate TPR ON TP.PayerId = TPR.Payerid 
                                INNER JOIN tblMeeting TM ON TB.ClientId = TM.ClientId
                                LEFT JOIN tblEmpRate TER ON TER.EmpId = TM.EmpId
                                WHERE TB.IsActive = 1 AND TB.ClientId = @clientId AND 
                                TM.MeetingId = @meetingId AND TPR.IsActive = 1 AND TM.IsStatus = 1
                                AND TM.MeetingDate BETWEEN TPR.ValidFrom AND TPR.ValidTo ";

                IList<PayerListViewModel> result = (await connection.QueryAsync<PayerListViewModel>(sql, new
                {
                    @clientId = clientId,
                    @meetingId = meetingId
                })).ToList();

                obj.Data = result;
                obj.Result = result != null ? true : false;
                obj.Message = result != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }




        public async Task<ServiceResponse<int>> UpdateSchedule(UpdateBillingSchedule model)
        {
            ServiceResponse<int> sres = new ServiceResponse<int>();
            IDbTransaction transaction = null;

            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction(IsolationLevel.Serializable);
                    string _query = "Update tblMeetingRate SET BillingStatus=@BillingStatus Where MeetingRateId=@MeetingRateId";
                    int rowsAffected = cnn.Execute(_query,
                        model.ScheduleList.Select(x => new
                        {
                            @MeetingRateId = x,
                            @BillingStatus = model.BillingStatus

                        }), transaction);
                    transaction.Commit();
                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = rowsAffected;
                        sres.Message = "Sucessfully  Updated.";
                    }
                    else
                    {
                        sres.Data = 0;
                        sres.Message = "Failed to Update.";
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













        public async Task<ServiceResponse<int>> CreateInvoice(InvoiceModel model)
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
                    string _query = @"INSERT INTO tblInvoice (ClientId, InvoiceNO, InvoiceAmount, InvoiceStatus, CreatedBy, CreatedOn) 
VALUES(@ClientId, @InvoiceNO, @InvoiceAmount, @InvoiceStatus, @CreatedBy, @CreatedOn); select SCOPE_IDENTITY();";



                    model.InvoiceId = (int)(cnn.ExecuteScalar<int>(_query, new
                    {
                        ClientId = model.ScheduleList.FirstOrDefault().ClientId,
                        InvoiceNO = model.InvoiceNo,
                        InvoiceAmount = model.ScheduleList.Sum(x => (x.BalanceAmount)),
                        InvoiceStatus = model.InvoiceStatus,
                        CreatedBy = model.CreatedBy,
                        CreatedOn = model.CreatedOn
                    }, transaction));
                    string sqlQuery = @"INSERT INTO tblInvoiceSchedule
(InvoiceId,MeetingId,ScheduleCost,PaymentStatus,CreatedOn,CreatedBy)
VALUES (@InvoiceId,@MeetingId,@ScheduleCost,@PaymentStatus,@CreatedOn,@CreatedBy);
Update tblMeetingRate SET BillingStatus=@BillingStatus Where MeetingId=@MeetingId";
                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery,
                        model.ScheduleList.Select(x =>
                        new
                        {
                            @BillingStatus = model.InvoiceStatus,
                            @InvoiceId = model.InvoiceId,
                            @MeetingId = x.MeetingId,
                            ScheduleCost = x.BalanceAmount,
                            PaymentStatus = model.IsActive,
                            CreatedBy = model.CreatedBy,
                            CreatedOn = model.CreatedOn
                        }), transaction);








                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = rowsAffected;
                        sres.Message = "Sucessfully  Updated.";
                    }
                    else
                    {
                        sres.Data = 0;
                        sres.Message = "Failed to Update.";
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










        public async Task<ServiceResponse<IEnumerable<InvoiceView>>> GetScheduleInvoice()
        {
            ServiceResponse<IEnumerable<InvoiceView>> obj = new ServiceResponse<IEnumerable<InvoiceView>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"Select y.MeetingRateId as ScheduleRateId,z.BillTo as PayerId, xz.PayerName, x.MeetingId, x.ClientId,
xx.FirstName +' '+ ISNULL(xx.MiddleName,'')+' ' + xx.LastName as ClientName,
x.EmpId,xy.FirstName +' '+ ISNULL(xy.MiddleName,'')+' ' + xy.LastName as EmpName,
x.MeetingDate as ServiceDate,x.IsCompleted as ScheduleStatus,y.BillingCode,y.BillingRate,y.BillingUnits,y.BillingTotal,y.BillingStatus,
y.PayrollPayStatus as PayrollStatus, p.InvoiceNo,p.InvoiceAmount,p.InvoiceStatus,q.ScheduleCost,q.PaymentStatus,p.CreatedOn as InvoiceDate from tblInvoice p Inner join tblInvoiceSchedule q on p.InvoiceId=q.InvoiceId
inner join tblMeeting x on q.MeetingId=x.MeetingId 
inner join tblMeetingRate y on x.MeetingId=y.MeetingId
inner join tblUser xx on x.ClientId= xx.UserId
inner join tblUser xy on x.EmpId= xy.UserId
inner Join tblClient z on x.ClientId= z.UserId
inner join tblPayer xz on z.BillTo= xz.PayerId";
                IEnumerable<ScheduleInvoiceModel> ObjData = (await connection.QueryAsync<ScheduleInvoiceModel>(sql));

                IEnumerable<InvoiceView> result = ObjData.GroupBy(x => x.InvoiceId).Select(y => new InvoiceView
                {
                    InvoiceId = y.Key,
                    InvoiceNo = y.FirstOrDefault().InvoiceNo,
                    InvoiceStatus = y.FirstOrDefault().InvoiceStatus,
                    InvoiceDate= y.FirstOrDefault().InvoiceDate,
                    ClientId = y.FirstOrDefault().ClientId,
                    ClientName = y.FirstOrDefault().ClientName,
                    PayerId = y.FirstOrDefault().PayerId,
                    PayerName = y.FirstOrDefault().PayerName,
                    Amounts = y.Sum(z => z.BillingTotal),
                    ScheduleList = y.ToList(),

                });
                obj.Data = result;
                obj.Result = result.Any() ? true : false;
                obj.Message = result.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }










    }

}
