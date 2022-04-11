using System;

namespace ES_HomeCare_API.Model.Client
{
    public class Medicationcs
    {
        public int MedicationID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string MedicationText { get; set; }
        public string NDCText { get; set; }
        public string StrengthText { get; set; }
        public string DosageText { get; set; }
        public string FrequencyText { get; set; }

        public string RouteText { get; set; }
        public string TabsText { get; set; }
        public string PrescriberText { get; set; }
        public string ClassificationText { get; set; }
        public string InstructionsText { get; set; }

        public bool Reminderscheck { get; set; }
        public bool Instructionscheck { get; set; }
        public bool administrationcheck { get; set; }
        public bool selfadministercheck { get; set; }
      
        public int ClientID { get; set; }
        public int CreatedBy { get; set; }
        public string createdOn { get; set; }
        public bool IsActive { get; set; }
    }

}
