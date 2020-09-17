using System;

namespace Dmt.Dm.Domain.Dto.Orders
{
    public class SubmitDataInput
    {
        public string id { get;set;}
        public string patientId { get; set; }
        public string orderType { get; set; } = "药疗";
        public DateTime? orderStartTime { get; set; } = DateTime.Now;
        public string orderCode { get; set; }
        public float? orderAmount { get; set; }
        public bool? isTemporary { get; set; } = true;
        public string orderFrequency { get; set; }
        public string orderAdministration { get; set; }
        public string memo { get; set; }
    }
}