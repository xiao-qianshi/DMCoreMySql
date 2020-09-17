using System;

namespace Dmt.Dm.Domain.Dto.Machine.MachineDisinfection
{
    public class Step2SignInput
    {
        public string id { get; set; }
        public DateTime? WipeStartTime { get; set; }
        public DateTime? WipeEndTime { get; set; }
        public bool? Option6 { get; set; }
        public string Option6Value { get; set; }
    }
}