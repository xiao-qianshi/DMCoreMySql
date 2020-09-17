using System;

namespace Dmt.DM.Mapper.Dto.Nurse
{
    public class GetGridJsonInput
    {
        public string patientId { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string pagination { get; set; }
    }
}