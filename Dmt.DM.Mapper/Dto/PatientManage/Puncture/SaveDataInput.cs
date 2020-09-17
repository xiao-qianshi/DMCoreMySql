namespace Dmt.DM.Mapper.Dto.PatientManage.Puncture
{
    public class SaveDataInput
    {
        public string VisitId { get; set; }
        public string Point1 { get; set; }
        public string Point2 { get; set; }
        public string Memo { get; set; }
        public bool IsSuccess { get; set; }
    }
}
