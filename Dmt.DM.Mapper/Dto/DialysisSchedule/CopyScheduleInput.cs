using System;
namespace Dmt.Dm.Domain.Dto.DialysisSchedule
{
    public class CopyScheduleInput
    {
        public DateTime? sourceDate { get; set; }
        public DateTime? targetDate { get; set; }
        public bool? isReplaced { get; set; } = false;
    }
}