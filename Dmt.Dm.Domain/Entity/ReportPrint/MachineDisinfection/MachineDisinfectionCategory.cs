using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint.MachineDisinfection
{
    public class MachineDisinfectionCategory
    {
        public List<MachineInfo> MachineInfos { get; set; }
        public MachineDisinfectionCategory()
        {
            MachineInfos = new List<MachineInfo>();
        }
    }
    public class MachineInfo
    {
        public string DialylisBedNo { get; set; }
        public string GroupName { get; set; }
        public string MachineName { get; set; }
        public string MachineNo { get; set; }
        public string Mid { get; set; }
        //public string ShowOrder { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<DisinfectionInfo> DisinfectionInfos { get; set; }
        public MachineInfo()
        {
            DisinfectionInfos = new List<DisinfectionInfo>();
        }
    }
    public class DisinfectionInfo
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
        /// <summary>
        /// 水路消毒开始时间
        /// </summary>
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        //public string Option1 { get; set; }
        //public string Option1Value { get; set; }
        //public string Option2 { get; set; }
        //public string Option2Value { get; set; }
        //public string Option3 { get; set; }
        /// <summary>
        /// 水路消毒方式
        /// </summary>
        public string DisinfectType { get; set; }
        //public string DisinfectDrug { get; set; }
        //public string Option4 { get; set; }
        //public string Option5 { get; set; }
        //public string Option6 { get; set; }
        //public string Option6Value { get; set; }
        /// <summary>
        /// 表面消毒方式
        /// </summary>
        public string SurfaceType { get; set; }
        public string WipeStartTime { get; set; }
        public string WipeEndTime { get; set; }
        public string Memo { get; set; }
        public string OperatePerson { get; set; }
        public string CheckPerson { get; set; }
    }
}
