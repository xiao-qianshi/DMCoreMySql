using System;

namespace Dmt.DM.Mapper.Dto.MachineManage.MachineProcess
{
    public class CreateReportInput
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Ids { get; set; }
        public bool IsMerge { get; set; }
    }
}
