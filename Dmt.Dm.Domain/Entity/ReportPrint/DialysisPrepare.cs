using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint
{
    /// <summary>
    /// 治疗单打印
    /// </summary>
    public class DialysisPrepare
    {
        public string HospialName { get; set; }
        public byte[] HospialLogo { get; set; }
        public List<PrepareRecord> PrepareRecords { get; set; }
        public List<Summation> Summations { get; set; }
    }
    /// <summary>
    /// 按照日期 和班次 明细及汇总
    /// </summary>
    public class PrepareRecord
    {
        public string VisitDate { get; set; }
        public string VisitNo { get; set; }
        public List<PrepareRecordDetail> PrepareRecordDetails{get; set;}
        public List<PrepareSumDetail> PrepareSumDetails { get; set; }
    }
    /// <summary>
    /// 日期和班次 汇总
    /// </summary>
    public class PrepareRecordDetail
    {
        public string Name { get; set; }
        public string DialysisNo { get; set; }
        public string GroupName { get; set; }
        public string DialysisBedNo { get; set; }
        public string DialysisType { get; set; }
        public string DialyzerType1 { get; set; }
        public string DialyzerType2 { get; set; }
        public string VascularAccess { get; set; }
        public string AccessName { get; set; }
        public string HeparinType { get; set; }
        public string HeparinAmount { get; set; }
        public string HeparinUnit { get; set; }
        public string HeparinAddAmount { get; set; }
        public string HeparinAddSpeedUnit { get; set; }
    }
    /// <summary>
    /// 日期和班次 明细
    /// </summary>
    public class PrepareSumDetail
    {
        public string ItemClass { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; }
    }

    /// <summary>
    /// 按照日期汇总
    /// </summary>
    public class Summation
    {
        public string ItemClass { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; }
    }
}
