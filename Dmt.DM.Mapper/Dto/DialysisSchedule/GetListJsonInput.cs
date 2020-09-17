using System;

namespace Dmt.Dm.Domain.Dto.DialysisSchedule
{
    public class GetListJsonInput
    {
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime startDate { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime endDate { get; set; }
        /// <summary>
        /// 患者Id
        /// </summary>
        public string patientId { get; set; }
        /// <summary>
        /// 床号ID
        /// </summary>
        public string bedId { get; set; }
        /// <summary>
        /// 第一班
        /// </summary>
        public bool? firstFlag { get; set; }
        /// <summary>
        /// 第二班
        /// </summary>
        public bool? secondFlag { get; set; }
        /// <summary>
        /// 第三班
        /// </summary>
        public bool? thirdFlag { get; set; }
    }
}