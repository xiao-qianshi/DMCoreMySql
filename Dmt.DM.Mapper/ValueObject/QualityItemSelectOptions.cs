namespace Dmt.DM.Mapper.ValueObject
{
    public class QualityItemSelectOptions
    {
        public string Id { get; set; }
        public string ItemType { get; set; }
        public int? OrderNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string HisItemCode { get; set; }
        public string Unit { get; set; }
        public string Result { get; set; }
        public float? LowerValue { get; set; }
        public float? UpperValue { get; set; }
        public float? LowerCriticalValue { get; set; }
        public float? UpperCriticalValue { get; set; }
        public bool? ResultType { get; set; }
        public string Memo { get; set; }
    }
}
