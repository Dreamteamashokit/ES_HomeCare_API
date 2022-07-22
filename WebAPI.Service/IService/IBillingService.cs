﻿using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Billing;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface IBillingService
    {
        Task<ServiceResponse<string>> AddPayer(PayerModel _model);
        Task<ServiceResponse<string>> UpdatePayer(PayerModel _model);
        Task<ServiceResponse<IEnumerable<PayerModel>>> GetPayerList();
        Task<ServiceResponse<string>> DelPayer(int PayerId);
        Task<ServiceResponse<BillingSummaryInfoModel>> GetBillingSummaryInfo(int userId);
        Task<ServiceResponse<IEnumerable<BillingStatusViewModel>>> GetBillingStatusList();
        Task<ServiceResponse<IEnumerable<PayrollStatusViewModel>>> GetPayrollStatusList();
        Task<ServiceResponse<IEnumerable<ClientSchedule>>> GetScheduleBilling();
        Task<ServiceResponse<IEnumerable<ScheduleBillingModel>>> GetScheduleBilling(SearchSchedule model);
        Task<ServiceResponse<BillingPayerRateViewModel>> GetBillingPayerRate(long payerId, long clientId, long meetingId);
    }
}