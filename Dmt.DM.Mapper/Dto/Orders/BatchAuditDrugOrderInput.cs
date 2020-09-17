using System;

namespace Dmt.DM.Mapper.Dto.Orders
{
    public class BatchAuditDrugOrderInput
    {
        public string PatientId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
