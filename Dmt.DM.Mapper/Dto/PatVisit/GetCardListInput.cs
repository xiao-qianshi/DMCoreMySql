using System;

namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetCardListInput
    {
        public DateTime visitDate { get; set; } = DateTime.Today.Date;
        public int visitNo { get; set; } = 1;
        public string groupName { get; set; } = "A";
    }
}