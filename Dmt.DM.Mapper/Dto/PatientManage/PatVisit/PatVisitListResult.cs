namespace Dmt.DM.Mapper.Dto.PatientManage.PatVisit
{
    public class PatVisitListResult
    {
        public string Id { get; set; }
        public string Pid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string VisitDate { get; set; }
        public int VisitNo { get; set; }
        public string GroupName { get; set; }
        public string DialysisBedNo { get; set; }
        public int DialysisNo { get; set; }
        public string DialysisType { get; set; }
        public string DialyzerType1 { get; set; }
        public string DialyzerType2 { get; set; }
        public string VascularAccess { get; set; }
        public string HeparinType { get; set; }
        public string BirthDay { get; set; }
        public string AgeDesc { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartPerson { get; set; }
        public string EndPerson { get; set; }
        public string CheckPerson { get; set; }
        public string PuncturePerson { get; set; }
        public string Py { get; set; }
        public bool IsArchive { get; set; }
        public bool IsAcct { get; set; }
    }
}
