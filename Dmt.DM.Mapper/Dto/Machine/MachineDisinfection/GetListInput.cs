using System;

namespace Dmt.Dm.Domain.Dto.Machine.MachineDisinfection
{
    public class GetListInput
    {
        public DateTime visitDate { get; set; } = DateTime.Today;
        public int visitNo { get; set; } = 1;   //班次(1全部，2第一班，4第二班，8第三班）
        public string groupNames { get; set; }
        public string bedNo { get; set; }
    }
}