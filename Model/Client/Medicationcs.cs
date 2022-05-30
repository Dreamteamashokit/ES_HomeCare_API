using System;

namespace ES_HomeCare_API.Model.Client
{
    public class Medicationcs
    {
        public int MedicationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MedicationText { get; set; }
        public string NDCText { get; set; }
        public string StrengthText { get; set; }
        public string DosageText { get; set; }
        public short FrequencyText { get; set; }
        public string RouteText { get; set; }
        public string TabsText { get; set; }
        public string PrescriberText { get; set; }
        public string ClassificationText { get; set; }
        public string InstructionsText { get; set; }
        public int ClientID { get; set; }
        public int IsActive { get; set; }
        public bool RemindersCheck { get; set; }
        public bool InstructionsCheck { get; set; }
        public bool AdministrationCheck { get; set; }
        public bool SelfAdministerCheck { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
