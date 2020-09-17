using System;

namespace Dmt.Dm.Domain.Dto.Puncture
{
    public class SubmitDataInput
    {
        public string id { get; set; }
        public string patientId { get; set; }
        public DateTime? operateTime { get; set; }
        public string point1 { get; set; }
        public string point2 { get; set; }
        public bool isSucess { get; set; } = true;
        public string memo { get; set; }
    }
}