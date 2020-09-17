using System;

namespace Dmt.Dm.Domain.Dto.Puncture
{
    public class GetGridJsonInput
    {
        public string pid { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string pagination { get; set; }
    }
}