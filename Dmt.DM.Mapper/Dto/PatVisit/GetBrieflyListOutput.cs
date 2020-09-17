using System;
using System.Collections.Generic;

namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetBrieflyListOutput
    {
        public string monthDesc { get; set; }
        public int count { get; set; }
        public List<BrieflyItem> items { get; set; }
        public GetBrieflyListOutput()
        {
            items = new List<BrieflyItem>();
        }
    }

    public class BrieflyItem
    {
        public string id { get; set; }
        public DateTime visitDate { get; set; }
        public int visitNo { get; set; }
        public string groupName { get; set; }
        public string bedNo { get; set; }
        public string dialysisType { get; set; }
        public DateTime dialysisStartTime { get; set; }
        public DateTime dialysisEndTime { get; set; }
        public string startTimeStr { get; set; }
        public string endTimeStr { get; set; }
        public string vascularAccess { get; set; }
        public string accessName { get; set; }
        public string dialyzerType { get; set; }
        public string heparinType { get; set; }
        public float heparinAmount { get; set; }
        public string heparinUnit { get; set; }
        public float heparinAddAmount { get; set; }
        public string heparinAddSpeedUnit { get; set; }
        public float weightYT { get; set; }
        public float weightTQ { get; set; }
        public float weightTH { get; set; }
        public float weightST { get; set; }
        public List<DrugOrderItem> orders { get; set; }
        public BrieflyItem()
        {
            orders = new List<DrugOrderItem>();
        }
    }

    public class DrugOrderItem
    {
        public string drugName { get; set; }
        public float drugAmount { get; set; }
        public string drugUnit { get; set; }
    }
}