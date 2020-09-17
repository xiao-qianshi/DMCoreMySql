using System;
namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetBrieflyListInput
    {
        public string keyValue { get; set; }
        public DateTime startDate { get; set; } = DateTime.Today.Date.AddDays(1 - DateTime.Today.Day);
        public DateTime endDate { get; set; } = DateTime.Today.Date;
        public string dialysisType { get; set; }
    }
}