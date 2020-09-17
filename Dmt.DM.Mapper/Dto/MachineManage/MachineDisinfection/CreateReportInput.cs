using System;

namespace Dmt.DM.Mapper.Dto.MachineManage.MachineDisinfection
{
    public class CreateReportInput
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Ids { get; set; }
        public bool IsMerge { get; set; }
    }
}
