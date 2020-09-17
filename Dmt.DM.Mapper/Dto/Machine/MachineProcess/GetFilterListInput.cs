using System;

namespace Dmt.Dm.Domain.Dto.Machine.MachineProcess
{
    public class GetFilterListInput
    {
        /// <summary>
        /// 床位ID
        /// </summary>
        public string keyValue { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? startDate { get; set; } = DateTime.Today;
        /// <summary>
        /// 截至日期
        /// </summary>
        public DateTime? endDate { get; set; } = DateTime.Today;
    }
}