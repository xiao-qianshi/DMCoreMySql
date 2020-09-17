using System;

namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetSubmitOrderJsonInput
    {
        public string id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}