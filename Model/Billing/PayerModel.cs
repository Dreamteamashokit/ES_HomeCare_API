using System;

namespace ES_HomeCare_API.Model.Billing
{
    public class PayerModel
    {
        public int PayerId { get; set; }
        public string PayerName { get; set; }
        public string BillToName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string NPI { get; set; }
        public string FedId { get; set; }
        public string ETIN { get; set; }
        public string Taxonomy { get; set; }
        public string MedicaidId { get; set; }
        public short IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
