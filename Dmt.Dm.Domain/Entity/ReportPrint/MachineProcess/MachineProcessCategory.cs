using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint.MachineProcess
{
    public class MachineProcessCategory
    {
        public List<ProcessSummeryInfo> ProcessSummeryInfos { get; set; }
        public MachineProcessCategory()
        {
            ProcessSummeryInfos = new List<ProcessSummeryInfo>();
        }
    }

    public class ProcessSummeryInfo
    {
        public string DialylisBedNo { get; set; }
        public string GroupName { get; set; }
        public string MachineName { get; set; }
        public string MachineNo { get; set; }
        public string Mid { get; set; }
        //public string ShowOrder { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ProcessItem> ProcessItems { get; set; }
        public ProcessSummeryInfo()
        {
            ProcessItems = new List<ProcessItem>();
        }
    }
    public class ProcessItem
    {
        public string VisitDate { get; set; }
        public string VisitNo { get; set; }
        /// <summary>
        /// 透析开始-截至时间
        /// </summary>
        public string DialysisStartTime { get; set; }
        public string DialysisEndTime { get; set; }
        public string PGender { get; set; }
        public string PName { get; set; }
        public string OperateTime { get; set; }
        public bool Option1 { get; set; }
        public bool Option2 { get; set; }
        public bool Option3 { get; set; }
        public bool Option4 { get; set; }
        public string Option5 { get; set; }
        public string Option6 { get; set; }
        public string Memo { get; set; }
        public string OperatePerson { get; set; }
    }
}
