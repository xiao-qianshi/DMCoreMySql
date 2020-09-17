namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class UpdateParameterInput
    {
        public string id { get; set; }
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
}