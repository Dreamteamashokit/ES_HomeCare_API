namespace ES_HomeCare_API.ViewModel.Employee
{
    public class ExternalLoginViewModel
    {
        public int userId { get; set; }
        public int userTypeId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string email { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}
