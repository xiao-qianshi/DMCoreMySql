using System;

namespace Dmt.Dm.Domain.Dto.DialysisSchedule
{
    public class GetSchedulePatientListInput
    {
        public DateTime? visitDate { get; set; } = DateTime.Today;
        public int visitNo { get; set; } = 1;
    }
}