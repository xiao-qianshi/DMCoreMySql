using System;

namespace Dmt.Dm.Domain.Dto.DialysisSchedule
{
    public class GetWeeklyListInput
    {
        public DateTime? scheduleDate { get; set; } = DateTime.Today;
        public int? visitNo { get; set; } = 1;
        public string dialysisTypes { get; set; }
        public string patientId { get; set; }
    }
}