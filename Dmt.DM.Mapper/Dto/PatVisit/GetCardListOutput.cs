using System;

namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetCardListOutput
    {
        public string id { get; set; }
        public string patientId { get; set; }
        public string patientName { get; set; }
        public int? patientAge { get; set; }
        public string headIcon { get; set; }
        public bool beInfected { get; set; }
        public string groupName { get; set; }
        public string bedNo { get; set; }
        public string dialysisType { get; set; }
        public DateTime? dialysisStartTime { get; set; }
        public DateTime? dialysisEndTime { get; set; }
        public string vascularAccess { get; set; }
        public string accessName { get; set; }
        public string dialyzerType { get; set; }
        public string heparinType { get; set; }
        public float? heparinAmount { get; set; }
        public string heparinUnit { get; set; }
        public float percent { get; set; }
        public int sortNo { get; set; }
    }
}