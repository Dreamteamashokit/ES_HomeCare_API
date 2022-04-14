namespace ES_HomeCare_API.Model.Account
{
    public class LoginModel
    {
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        
    }



    public class UserModel
    {
        public int UserId { get; set; }
        public long LoginInId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
       

    }


















}
