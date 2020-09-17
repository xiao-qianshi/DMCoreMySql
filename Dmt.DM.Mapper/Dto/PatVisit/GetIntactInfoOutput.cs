using System;
using System.Collections.Generic;

namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetIntactInfoOutput
    {        
        public DateTime? dialysisStartTime { get; set; }
        public DateTime? dialysisEndTime { get; set; }
        public float percent { get; set; } = 0;
        public Patients patient { get; set; }
        public VitalSigns vitalSigns { get; set; }
        public DialysisParameter dialysisParameter { get; set; }
        public WeightSet weightSet { get; set; }
        public PunctureRecord punctureRecord { get; set; }
        public List<DoctorOrder> doctorOrders { get; set; }
        public List<NurserOrder> nurserOrders { get; set; }
        public List<Observations> observations { get; set; }
        public string puncturePerson { get; set; }
        public string startPerson { get; set; }
        public string checkPerson { get; set; }
        public string endPerson { get; set; }
        public string memo { get; set; }
        public GetIntactInfoOutput()
        {
            doctorOrders = new List<DoctorOrder>();
            nurserOrders = new List<NurserOrder>();
            observations = new List<Observations>();
        }
    }
    public class Patients
    {
        public string patientId { get; set; }
        public string patientName { get; set; }
        public string patientGender { get; set; }
        public int? patientAge { get; set; }
        public string maritalStatus { get; set; }
        public string headIcon { get; set; }
        public float? ideaWeight { get; set; }
    }
    public class VitalSigns
    {
        public float? systolicPressure { get; set; }
        public float? diastolicPressure { get; set; }
        public float? pulse { get; set; }
        public float? temperature { get; set; }
    }
    public class DialysisParameter
    {
        public string dialysisType { get; set; }
        public string dilutionType { get; set; }
        public float? exchangeAmount { get; set; }
        public float? exchangeSpeed { get; set; }
        public float? bloodSpeed { get; set; }
        public float? dialysateTemperature { get; set; }
        public float? estimateHours { get; set; }
        public string vascularAccess { get; set; }
        public string accessName { get; set; }
        public string dialyzerType1 { get; set; }
        public string dialyzerType2 { get; set; }
        public string heparinType { get; set; }
        public float? heparinAmount { get; set; }
        public string heparinUnit { get; set; }
        public float? heparinAddAmount { get; set; }
        public string heparinAddSpeedUnit { get; set; }
        public bool? LowCa { get; set; }
        public float? Ca { get; set; }
        public float? K { get; set; }
        public float? Na { get; set; }
        public float? Hco3 { get; set; }     
    }
    public class WeightSet
    {
        public float? weightTQ { get; set; }
        public float? weightYT { get; set; }
        public float? weightTH { get; set; }
        public float? weightST { get; set; }
    }
    public class PunctureRecord
    {
        public string point1 { get; set; }
        public string point2 { get; set; }
    }
    public class DoctorOrder
    {
        public string id { get; set; }
        public bool? isTemporary { get; set; }
        public string orderText { get; set; }
        public string orderUnitSpec { get; set; }
        public float? orderAmount { get; set; }
        public string orderFrequency { get; set; }
        public string orderAdministration { get; set; }
        public int? orderStatus { get; set; }
    }
    public class NurserOrder
    {
        public string id { get; set; }
        public bool? isTemporary { get; set; }
        public string orderText { get; set; }
        public string orderUnitSpec { get; set; }
        public float? orderAmount { get; set; }
        public string orderFrequency { get; set; }
        public string orderAdministration { get; set; }
        public DateTime? execTime { get; set; }
        public string nurseName { get; set; }
    }
    public class Observations
    {
        public string id { get; set; }
        public float? ssy { get; set; }
        public float? szy { get; set; }
        public float? hr { get; set; }
        public float? a { get; set; }
        public float? bf { get; set; }
        public float? ufr { get; set; }
        public float? v { get; set; }
        public float? c { get; set; }
        public float? t { get; set; }
        public float? ufv { get; set; }
        public float? tmp { get; set; }
        public float? gsl { get; set; }
        public string memo { get; set; }
        public string nurseName { get; set; }
        public DateTime? operatorTime { get; set; }
    }
}