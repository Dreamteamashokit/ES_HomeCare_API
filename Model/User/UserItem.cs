using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model.Employee;

namespace ES_HomeCare_API.Model.User
{
    public class UserItem
    {
        public long UserId { get; set; }
        public string UserKey { get; set; }
        public short UserType { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Organization { get; set; }
        public string SSN { get; set; }
        public NameClass Name { get; set; }
        public int? SupervisorId { get; set; }
        public NameClass Supervisor { get; set; }
        public string DOB { get; set; }
        public short Gender { get; set; }
        public short MaritalStatus { get; set; }
        public short Ethnicity { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string EmergPhone { get; set; }
        public string EmergContact { get; set; }
        public AddressView Address { get; set; }
    }
}
