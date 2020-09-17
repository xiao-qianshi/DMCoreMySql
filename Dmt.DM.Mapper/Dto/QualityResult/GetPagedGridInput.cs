using System;

namespace Dmt.Dm.Domain.Dto.QualityResult
{
    public class GetPagedGridInput
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get; set; } = 30;
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string orderField { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string orderType { get; set; } = "asc";
        /// <summary>
        /// 项目代码
        /// </summary>
        public string keyValue { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime startDate { get; set; } = DateTime.Today.Date.AddDays(1 - DateTime.Today.Day);
        /// <summary>
        /// 截至时间
        /// </summary>
        public DateTime endDate { get; set; } = DateTime.Today.Date;
        /// <summary>
        /// 患者ID
        /// </summary>
        public string patientId { get; set; }
    }
}