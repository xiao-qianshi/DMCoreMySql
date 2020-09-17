namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class UpdateVitalSignsInput
    {
        public string id { get; set; }
        public float? systolicPressure { get; set; }
        public float? diastolicPressure { get; set; }
        public float? pulse { get; set; }
        public float? temperature { get; set; }
    }
}