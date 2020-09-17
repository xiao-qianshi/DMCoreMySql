using System.Collections.Generic;

namespace Dmt.Dm.Domain.Dto.DialysisSchedule
{
    public class GetFilterListOutput
    {
        public string monthDesc { get; set; }
        public List<ScheduleItem> items { get; set; }
        public GetFilterListOutput()
        {
            items = new List<ScheduleItem>();
        }
    }

    public class ScheduleItem
    {
        public string dayDesc { get; set; }
        public string weekDesc { get; set; }
        public int visitNo { get; set; }
        public string groupName { get; set; }
        public string dialysisBedNo { get; set; }
        public string dialysisType { get; set; }
    }
}