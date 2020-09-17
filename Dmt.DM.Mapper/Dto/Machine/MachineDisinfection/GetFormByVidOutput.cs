using System;

namespace Dmt.Dm.Domain.Dto.Machine.MachineDisinfection
{
    public class GetFormByVidOutput
    {
        public string id { get; set; }
        public string Mid { get; set; }
        public string Pid { get; set; }
        public string PName { get; set; }
        public string PGender { get; set; }
        public string Vid { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? VisitNo { get; set; }
        public DateTime? DialysisStartTime { get; set; }
        public DateTime? DialysisEndTime { get; set; }
        public string GroupName { get; set; }
        public int? ShowOrder { get; set; }
        public string DialylisBedNo { get; set; }
        public string MachineNo { get; set; }
        public string MachineName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? WipeStartTime { get; set; }
        public DateTime? WipeEndTime { get; set; }
        public bool? Option1 { get; set; }
        public string Option1Value { get; set; }
        public bool? Option2 { get; set; }
        public string Option2Value { get; set; }
        public bool? Option3 { get; set; }
        public bool? Option4 { get; set; }
        public bool? Option5 { get; set; }
        public bool? Option6 { get; set; }
        public string Option6Value { get; set; }
        public string OperatePerson { get; set; }
        public string CheckPerson { get; set; }
        public string Memo { get; set; }
    }
}