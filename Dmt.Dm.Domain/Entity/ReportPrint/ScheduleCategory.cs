namespace Dmt.DM.Domain.Entity.ReportPrint
{
    public class ScheduleCategory
    {
        public string HospitalName { get; set; }
        public byte[] HospialLogo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class ScheduleCellItem
    {
        public string VisitDate { get; set; }
    }
}
