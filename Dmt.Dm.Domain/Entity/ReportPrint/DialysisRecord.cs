using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint.DialysisRecord
{
    /// <summary>
    /// 治疗单打印
    /// </summary>
    public class Category
    {
        public string HospialName { get; set; }
        public byte[] HospialLogo { get; set; }
        public string Name { get; set; }
        public string DialysisNo { get; set; }
        public string CardNo { get; set; }
        public string VisitDate { get; set; }
        public string VisitNo { get; set; }
        public string DialysisCount { get; set; }
        public string RecordNo { get; set; }
        public string PatientNo { get; set; }
        public string Gender { get; set; }
        public string AgeString { get; set; }
        public string BirthDay { get; set; }
        public string InsuranceNo { get; set; }
        public string IdNo { get; set; }
        public string MaritalStatus { get; set; }
        public string IdealWeight { get; set; }
        public string DialysisStartTime { get; set; }
        public string Diagnosis { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string Trasfer { get; set; }
        public string PhoneNo { get; set; }
        public string BedNo { get; set; }
        public List<PatVisitRecord> PatVisitRecords { get; set; }
        public List<Observation> Observations { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class PatVisitRecord
    {
        public string DialysisType { get; set; }
        public string DialyzerType1 { get; set; }
        public string DialyzerType2 { get; set; }
        public string EstimateHours { get; set; }
        public string BloodSpeed { get; set; }
        public string DialysateTemperature { get; set; }
        public string HeparinType { get; set; }
        public string VascularAccess { get; set; }
        public string VitalSigns { get; set; }
        public string PGwzsz { get; set; }
        public string PGwzcx { get; set; }
        public string WeightIdea { get; set; }
        public string WeightTQ { get; set; }
        public string WeightYT { get; set; }
        public string WeightTH { get; set; }
        public string RecordDoctor { get; set; }
        public string DJMH { get; set; }
        public string DialyzerStatus { get; set; }
        public string DialysisTime { get; set; }
        public string MachineShowAmount { get; set; }
        public string RealExchangeAmount { get; set; }
        public string DuctOpeningStatus { get; set; }
        public string FistulaStatus { get; set; }
        public string PuncturePerson { get; set; }
        public string StartPerson { get; set; }
        public string CheckPerson { get; set; }
        public string EndPerson { get; set; }
        public string Reason { get; set; }
        public string Memo { get; set; }
        
    }

    public class Observation
    {
        public string SSY { get; set; }
        public string SZY { get; set; }
        public string HR { get; set; }
        public string A { get; set; }
        public string BF { get; set; }
        public string UFR { get; set; }
        public string V { get; set; }
        public string C { get; set; }
        public string T { get; set; }
        public string UFV { get; set; }
        public string TMP { get; set; }
        public string GSL { get; set; }
        public string MEMO { get; set; }
        public string Nurse { get; set; }
        public string NurseOperatorTime { get; set; }
    }

    public class Order
    {
        public string OrderType { get; set; }
        public string OrderText { get; set; }
        public string OrderSpec { get; set; }
        public string OrderUnitAmount { get; set; }
        public string OrderUnitSpec { get; set; }
        public string OrderAmount { get; set; }
        public string OrderFrequency { get; set; }
        public string OrderAdministration { get; set; }
        public bool? IsTemporary { get; set; }
        public string Doctor { get; set; }
        public string DoctorAuditTime { get; set; }
        public string Nurse { get; set; }
        public string NurseOperatorTime { get; set; }
        public string Memo { get; set; }
    }
}
