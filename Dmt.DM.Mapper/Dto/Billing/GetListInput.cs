using System;

namespace Dmt.Dm.Domain.Dto.Billing
{
    public class GetListInput
    {
        public string keyValue { get; set; }
        public DateTime startDate { get; set; } = DateTime.Now.Date.AddDays(1 - DateTime.Today.Day);
        public DateTime endDate { get; set; } = DateTime.Now.Date;
        public string billType { get; set; }
        public string patientId { get; set; }
    }
}