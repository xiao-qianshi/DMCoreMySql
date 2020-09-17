using System;

namespace Dmt.DM.Code.Excel.Model
{
    public class ImportScheduleModel
    {
        public string F_GroupName { get; set; }
        public string F_DialysisBedNo { get; set; }
        public int DayOfWeek { get; set; }
        public int F_VisitNo { get; set; }
        public string F_Name { get; set; }
        public string F_DialysisType { get; set; }
        public string F_BId { get; set; }
        public DateTime F_VisitDate { get; set; }
        public string F_PId { get; set; }
        public int F_DialysisNo { get; set; } = 0;
        public int F_Sort { get; set; }
    }
}
