using System;

namespace Dmt.Dm.Domain.Dto.DialysisSchedule
{
    public class GetFilterListInput
    {
        public string patientId { get; set; }
        public DateTime startDate { get; set; } = DateTime.Today.Date.AddDays(1 - DateTime.Today.Day);
        public DateTime endDate { get; set; } = DateTime.Today.Date;
        public string dialysisType { get; set; }
    }
}