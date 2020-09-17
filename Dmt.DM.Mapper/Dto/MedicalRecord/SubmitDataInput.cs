using System;

namespace Dmt.Dm.Domain.Dto.MedicalRecord
{
    public class SubmitDataInput
    {
        public string id { get; set; }
        public string patientId { get; set; }
        public DateTime? contentTime { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}