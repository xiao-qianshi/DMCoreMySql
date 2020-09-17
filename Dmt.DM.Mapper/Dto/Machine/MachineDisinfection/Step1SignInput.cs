using System;

namespace Dmt.Dm.Domain.Dto.Machine.MachineDisinfection
{
    public class Step1SignInput
    {
        public string id { get; set; }    
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? Option1 { get; set; }
        public string Option1Value { get; set; }
        public bool? Option2 { get; set; }
        public string Option2Value { get; set; }
        public bool? Option3 { get; set; }
        public bool? Option4 { get; set; }
        public bool? Option5 { get; set; }
        public string Memo { get; set; }
    }
}