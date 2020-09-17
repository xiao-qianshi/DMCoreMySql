using System;
namespace Dmt.Dm.Domain.Dto.Orders
{
    public class GetDrugOrdersHistoryInput
    {
        /// <summary>
        /// 过滤描述
        /// </summary>
        public string patientId { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? startDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        /// <summary>
        /// 截至日期
        /// </summary>
        public DateTime? endDate { get; set; } = DateTime.Now.Date;
        /// <summary>
        /// 过滤描述
        /// </summary>
        public string keyword { get; set; }
    }
}