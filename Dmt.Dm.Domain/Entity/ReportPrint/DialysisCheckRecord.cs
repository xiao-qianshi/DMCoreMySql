using System;
using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint.DialysisRecord
{
    /// <summary>
    /// 护士核对治疗单
    /// </summary>
    public class DialysisCheckRecord
    {
        public DateTime VisitDate { get; set; }
        public int VisitNo { get; set; }
        public string GroupName { get; set; }  
        public List<CheckRecordItem> Items { get; set; }
    }
    /// <summary>
    /// 明细
    /// </summary>
    public class CheckRecordItem
    {
        public string Name { get; set; }
        public string BedNo { get; set; }
        public string HeparinType { get; set; }
        public string HeparinAmount { get; set; }
        public string HeparinUnit { get; set; }
        public string HeparinAddAmount { get; set; }
        public string DialysisType { get; set; }
        public string DialyzerType1 { get; set; }
        public string DialyzerType2 { get; set; }
    }
}
