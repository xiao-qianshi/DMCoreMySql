using System;

namespace Dmt.Dm.Domain.Dto.Nurse
{
    public class SubmitDataInput
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
        public DateTime? operatorTime { get; set; } = DateTime.Now;
    }
}