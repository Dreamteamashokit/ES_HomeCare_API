
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace ES_HomeCare_API.Model.Data
{
    // file name renamed
    public enum ModifiedType
    {        
        [Description("Percentage")]
        Percentage = 1,
        [Description("Fixed")]
        Fixed = 2

    }

    public enum TransactionType
    {
        [Description("Credit Limit")]
        CreditLimit = 1,
        [Description("Cash")]
        Cash,
        [Description("Cash + Credit Limit")]
        CashAndCredit,
        [Description("Temporary Limit")]
        TempLimit
    }
    public enum OperationType
    {
        [Description("Credit Limit Initiated")]
        CreditLimitInsert = 1,
        [Description("Cash Initiated")]
        CashInsert,
        [Description("Credit Limit Updated")]
        CreditLimitUpdate,
        [Description("Cash Updated")]
        CashUpdate,
        [Description("Manual Booking")]
        ManualBooking,
        [Description("Online Booking")]
        OnlineBooking,
        [Description("Booking Cancellation")]
        BookingCancellation,
        [Description("Bank Deposit")]
        BankDeposit,
        [Description("Manual Adjustment")]
        ManualAdjustment,
        [Description("DirectPayment")]
        DirectPayment,
        [Description("Booking Amendment")]
        BookingAmendment,
    }


    public enum PageType
    {
        [Description("My Profile")]
        MyProfile = 1,
        [Description("Manage User")]
        ManageUser,
    }


    /* for BankDeposit Request : Created By : Manisha Create On: 14/09/2016*/
    public enum PaymentMode
    {
        [Description("Payment By Cheque")]
        cheque = 1,
        [Description("Payment By Draft")]
        Draft = 2,
        [Description("Payment By Cash")]
        Cash = 3,
        [Description("Payment Online")]
        OnlinePayment = 4,
        [Description("POS")]
        POS = 5,

    }


    public enum BankRequestStatus
    {
        [Description("In Process")]
        InProcess = 1,
        [Description("Acknowledged")]
        Acknowledged = 2,
        [Description("Declined")]
        Declined = 3,




    }

    public enum BankRequestInprocess
    {

        [Description("Acknowledged")]
        Acknowledged = 2,
        [Description("Declined")]
        Declined = 3,




    }


    public enum BankRequestRevoke
    {
        [Description("ReversePayment")]
        ReversePayment = 4
    }


    /// <summary>
    /// This region  is  created by user Rakesh
    /// </summary>    
    #region Rakesh



    public enum OfferType
    {
        [Description("Markup")]
        AddMarkup = 1,
        [Description("Discount")]
        Discount = 2
      
    }

    public enum Status
    {
        [Description("In-Active")]
        InActive = 0,
        [Description("Active")]
        Active = 1,
        [Description("Suspended")]
        Suspended = 2,
        [Description("Deleted")]
        Deleted = 3,
    }
    public enum FolioStatus
    {

        [Description("In-Active")]
        InActive = 0,
        [Description("Active")]
        Active = 1,
        [Description("Suspended")]
        Suspended = 2,
        [Description("Deleted")]
        Deleted = 3,
    }













    public enum SupplierType
    {
        [Description("XML")]
        XML = 1,
        [Description("NON XML")]
        NONXML,

    }



    public enum BookingStatus
    {
        [Description("InProcess")]
        InProcess = 1,
        [Description("Confirmed")]
        Confirmed,
        [Description("Vouchered")]
        Vouchered,
        [Description("Cancelled")]
        Cancelled,
        [Description("Failed")]
        Failed,
        [Description("Cancelled at Agent")]
        CancelledAtAgent,
        [Description("Voucher Amended")]
        VoucherAmended,
        [Description("CNF Amended")]
        Amended,
        [Description("Manual Confirmed")]
        ManualConfirmed,
        [Description("Manual Vouchered")]
        ManualVouchered,
        [Description("Booked")]
        Booked,
        [Description("TktInProcess")]
        TktInProcess,
        [Description("Ticketed")]
        Ticketed,
        [Description("OnRequest")]
        OnRequest,
        [Description("Book On REQ")]
        BookOnReq,
        [Description("On Req Declined")]
        OnReqDeclined,
        [Description("PG Request")]
        PGRequest
    }



    public enum BookingType
    {
        [Description("Offline")]
        Offline = 1,
        [Description("Online")]
        Online,

    }

    public enum ServiceEnum
    {
        [Description("Hotel")]
        Hotel = 1,
        [Description("Flight")]
        Flight,
        [Description("Transfer")]
        Transfer,
        [Description("Sight Seeing")]
        Sight = 17,

    }

    public enum ChannelEnum
    {
        [Description("B2B")]
        B2B = 1,
        [Description("B2C")]
        B2C,
        [Description("XML Out")]
        XMLOut,
        [Description("Backoffice Branch")]
        BackofficeBranch,

    }






    public enum ReportType
    {
        [Description("Invoice")]
        Invoice = 1,
        [Description("Credit")]
        Credit,
        [Description("Booking")]
        Booking,
        [Description("Receipt")]
        Receipt,
        [Description("Folio")]
        Folio,
    }

    /************
     * UserEntity.DistributorSubAgentUser --> PropertyUser or Extranet User
     * */

    public enum UserEntity
    {
        [Description("Super Admin")]
        SuperAdmin = 1,
        [Description("Customer")]
        MainAgent,
        [Description("Branch")]
        Branch,
        [Description("Distributor")]
        Distributor,
        [Description("Corporate")]
        Corporate,
        [Description("Travel Agent")]
        BranchSubAgent,
        [Description("Travel Agent")]
        DistributorSubAgent,
        [Description("Staff User")]
        AdminUser,
        [Description("Staff User")]
        MainAgentUser,
        [Description("Staff User")]
        BranchUser,
        [Description("Staff User")]
        DistributorUser,
        [Description("Staff User")]
        BranchSubAgentUser,
        [Description("Staff User")]
        DistributorSubAgentUser,
        [Description("Staff User")]
        PropertyUser,
        [Description("Property User")]
        ManageUser
    }

    public enum GuestType
    {
        Adult = 1,
        Child,
        Infant
    }


    public enum Title
    {
        None = 0,
        Mr,
        Mrs,
        Master,
        Miss,
        Dr,
        Prof,
        Sheikh,
        Sheikha,
    }
    public enum TitleB2C
    {
        None = 0,
        Mr,
        Mrs,
        Master,
        Miss,
        Dr,
        Prof
      
    }

    public enum FundType
    {

        Dr = 1,
        Cr
    }
    public enum ExtPropertyType
    {
        //None = 0,
        Group = 0,
        Property
    }

    public enum MenuType
    {
        NonMenu = 0,
        Menu = 1

    }





    #endregion


    #region Navneet
    public enum Emailenum
    {
        //Trancation Category Email Notification Start
        Notification = 1,
        Confirmation,
        Voucher,
        Cancellation,
        Amendament,
        CancellationDueDate,
        VoucherCancellation,
        CreditlmtUpdateonCancel,
        FailedConfirmation,
        //Trancation Category Email Notification End

        //Profile Category Email Notification Start
        CreateUser = 10,
        ResetPassword,
        //Profile Category Email Notification End


        //Activation/Deactivation Category Email Notification Start
        CityAc_Deactive,
        CountryAc_Deactive,
        StateAc_Deactive,
        SrvAc_Deactive,
        CurrencyAc_Deactive,
        CultureAc_Deactive,
        ChnlAc_Deactive,
        SuplerAc_Deactive,
        //Activation/Deactivation Email Notification End


        //Markup Category Email Notification Start
        CurrencyMarkUp = 21,
        SupplierMarkup,
        //Markup Category Email Notification End



        //Restriction Category Email Notification Start
        CountryRestrct,
        StateRestrct,
        CityRestrct,
        SupplierRestrct,
        BranchRestrct,
        SubagentRestrct,
        //Restriction Category Email Notification End


        //CreditLimit Category Email Notification Start
        StatemntDatelmt,
        PaymentDueDatelmt,
        LowBalancelmt,
        BankDepositelmt,
        BankDepositeAclmt,

        //CreditLimit Category Email Notification End



        //Extranet Category Email Notification Start
        AllocationManagment,
        PromotionManagment,


        //Extranet Category Email Notification End
    }



    public enum XsltEnum
    {
        CreditNote = 1,
        GeneralNote,
        Invoice,
        Reciept,
        Vouchered,
        ForgetPassword,
        HtlExtAllocation,
        HtlExtPromotion,
        test,
        HtlExtBooking,
        HtlExtBookingCancel,
        htlExtPromotionEmailScheduler,
        TransferCreditNote,
        TransferInvoice,
        TransferVoucher,
        TransferReciept=17,
        Quotation=23
    }
    #endregion



    public enum ExceptionSource
    {
        B2B = 1,
        Extranet = 2,
        B2C = 3

    }
    #region Meena


    public enum AmendType
    {
        Nochange = 0,
        Invoice = 1,
        CreditNote = 2,


    }
    public enum TransferLanguage
    {
        
        [Description("Arabic")]
        Arabic=1,
        [Description("English ")]
        English,
        [Description("French")]
        French,
        [Description("Germany")]
        Germany,
        [Description("Portuguese")]
        Portuguese,
        
    }
    
  
    public enum TransferPickupPickOff
    {
        [Description("Airport")]
        Airport = 1,
        [Description("Accomodation")]
        Accomodation,
        [Description("Port")]
        Port,
        [Description("Station")]
        Station,
    }
    public enum TransferType
    {
        [Description("Single")]
        Single = 1,
        [Description("Round")]
        Round = 2
    }

    public enum AmendmentReqStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Accept")]
        Accept,
        [Description("Decline")]
        Decline,
        [Description("Suspended")]
        Suspended
    }

    #endregion

    #region Nupur
    public enum B2CUserType
    {
        BookAsGuest = 1,
        SignUp = 2,
        Login = 3
    }

    public enum StarRating
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        None
    }

   public enum PrefferedAccounting
    {

        [Description("Credit Type")]
        Credit = 1,
        [Description("Cash Type")]
        Cash = 2
    }

    public enum SghtTrnsfrTyp
    {
        None,
        Hatchback,
        Sedan,
        SUV,
        Luxury,
        Delux,
        Traveller
    }

    public enum PricingType
    {
        [Description("Flat")]
        Flat = 1,
        [Description("SeatWise")]
        SeatWise = 2
    }
    public enum BookingStatusExt
    {
        [Description("On-Request")]
        OnRequest = 0,
        [Description("Confirmed")]
        Confirmed,
        [Description("Cancelled")]
        Cancelled,
        [Description("Rejected(On-Request)")]
        Rejected,
        [Description("Confirmed(On-Request)")]
        ConfirmedOnReq,
        [Description("Cancelled(On-RequestCnf)")]
        CancelledOnReqCnf

    }
    #endregion
    #region Suraj
    public enum ClassTypeFlight
    {
        [Description("Any")]
        Any = 0,
        [Description("Economy")]
        Economy,
        [Description("Premium Economy")]
        PremiumEconomy,
        [Description("Business")]
        Business,
        [Description("First")]
        First,
    }
    #endregion  


    #region Ajay

    public enum DocumentType
    {
        [Description("Credit Note")]
        CreditNote = 1,
        [Description("General Note")]
        GeneralNote = 2,
        [Description("Invoice")]
        Invoice = 3,
        [Description("Reciept")]
        Reciept = 4,
        [Description("Voucher")]
        Voucher = 5,
        [Description("Forget Password")]
        ForgetPassword = 6,
        [Description("htlExtAllocation")]
        htlExtAllocation = 7,
        [Description("htlExtPromotion")]
        htlExtPromotion = 8,
        [Description("HtlExtBooking")]
        HtlExtBooking = 10,
        [Description("HtlExtBookingCancel")]
        HtlExtBookingCancel = 11,
        [Description("htlExtPromotionEmailScheduler")]
        htlExtPromotionEmailScheduler = 12,
        [Description("TransferCreditNote")]
        TransferCreditNote = 13,
        [Description("TransferInvoice")]
        TransferInvoice = 14,
        [Description("TransferVoucher")]
        TransferVoucher = 15,
        [Description("Exception in Case of Boking Failure At Supplier")]
        ExceptioninCaseofBokingFailureAtSupplier = 16,
        [Description("TransferReciept")]
        TransferReciept = 17,
        [Description("Exception Handling")]
        ExceptionHandling = 18,
        [Description("PaymentRequest")]
        PaymentRequest = 22,
        [Description("Quotation")]
        Quotation = 23,
        [Description("htlExtSpclPrice")]
        htlExtSpclPrice = 24,
        [Description("htlExtDiscountLog")]
        htlExtDiscountLog = 25,
        [Description("htlExtStayPayLog")]
        htlExtStayPayLog = 26,
        [Description("htlExtSPWDLog")]
        htlExtSPWDLog = 27,
        [Description("htlExtSPWSP")]
        htlExtSPWSP = 28,
        [Description("htlExtSpDeal")]
        htlExtSpDeal = 29

    }
    public enum FlightBookingStatus
    {
        [Description("InProcess")]
        InProcess = 1,
        [Description("OnHold")]
        OnHold,
        [Description("Cancelled")]
        Cancelled,
        [Description("Failed")]
        Failed,
        [Description("Ticket In Process")]
        TktInProcess,
        [Description("Ticketed")]
        Ticketed,
        [Description("Ticket Amended")]
        TicketAmended          
    }
    public enum FlightTicketType
    {
        [Description("ETICKET")]
        ETICKET = 1
    }
    public enum FlightFareType
    {
        [Description("PUBLIC")]
        PUBLIC = 1,
        [Description("PRIVATE")]
        PRIVATE = 2,
        [Description("WEBFARE")]
        WEBFARE = 3
    }
    public enum FlightIsPassportMandatory
    {
        [Description("FALSE")]
        FALSE = 0,
        [Description("TRUE")]
        TRUE,
    }
    public enum FlightIsRefundable
    {
        [Description("NO")]
        NO = 0,
        [Description("YES")]
        YES,
        [Description("INFORMATIONNOTAVAILABLE")]
        INFORMATIONNOTAVAILABLE
    }
    public enum FlightSegmentType
    {
        [Description("ONWARD")]
        ONWARD = 1,
        [Description("RETURN")]
        RETURN = 2,
    }
    public enum FlightPaxType
    {
        ADT = 1,
        CHD,
        INF
    }
    public enum FlightDurationType
    {
        [Description("MINUTES")]
        MINUTES = 1,

    }
    public enum FlightType
    {
        [Description("OneWay")]
        OneWay = 1,
         [Description("Round-Trip")]
        RoundTrip,
         [Description("Multi-City")]
        MultiCity

    }
    public enum PGTxnStatus
    {
        [Description("InProcess")]
        InProcess = 1,
        [Description("Successful")]
        Successful,
        [Description("Failed")]
        Failed,
    }
    public enum BookingCxlReqStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Accepted")]
        Accepted,
        [Description("Declined")]
        Declined
       
    }
    public enum FltTktGenerationType
    {
        [Description("Manual")]
        Manual = 1,
        [Description("Service")]
        Service,
        [Description("Offline")]
        Offline,
        [Description("Direct")]
        Direct
    }

    public enum FltSeatPreference
    {
        [Description("Any")]
        Any = 1,
        [Description("A")]
        A,
        [Description("W")]
        W        
    }
    public enum FltMealPreference
    {
        [Description("Any")]
        Any = 1,
        [Description("HFML")]
        HFML,
        [Description("LPML")]
        LPML,
        [Description("ORML")]
        ORML,
        [Description("PRML")]
        PRML,
        [Description("VJML")]
        VJML,
        [Description("VOML")]
        VOML,
        [Description("AVML")]
        AVML,
        [Description("BBML")]
        BBML,
        [Description("BLML")]
        BLML,
        [Description("FPML")]
        FPML,
        [Description("GFML")]
        GFML,
        [Description("LFML")]
        LFML,
        [Description("LSML")]
        LSML,
        [Description("NLML")]
        NLML,
        [Description("RVML")]
        RVML,
        [Description("VVML")]
        VVML,
        [Description("VLML")]
        VLML,
        [Description("KSML")]
        KSML,
        [Description("CHML")]
        CHML,
        [Description("MOML")]
        MOML,
        [Description("SFML")]
        SFML,
        [Description("HNML")]
        HNML,
        [Description("PFML")]
        PFML,
        [Description("JNML")]
        JNML,
        [Description("DBML")]
        DBML,
        [Description("FFML")]
        FFML,
        [Description("SPMLJ")]
        SPMLJ,
        [Description("LCML")]
        LCML,
        [Description("NSML")]
        NSML,
        [Description("VGML")]
        VGML
    }

    public enum PaymentGateway
    {
        [Description("MIGS")]
        MIGS = 1,
        [Description("VCS")]
        VCS = 2,
        [Description("SQUARE PAYMENTS")]
        SquarePayments = 3

    }

    #endregion

}

