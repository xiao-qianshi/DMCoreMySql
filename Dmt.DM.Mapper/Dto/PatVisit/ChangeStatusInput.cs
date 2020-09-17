using System;

namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class ChangeStatusInput
    {
        public string id { get; set; }
        public string operateType { get; set; }
        public DateTime? operateTime { get; set; }
    }
}