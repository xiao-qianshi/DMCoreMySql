using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dmt.Dm.Domain.Dto.InfectionControl.Infection
{
    public class GetPagedListInput
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
        public string orderType { get; set; } = "desc";
        /// <summary>
        /// 过滤描述
        /// </summary>
        public string keyValue { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? startDate { get; set; } = DateTime.Today.AddDays(-30);
        /// <summary>
        /// 截至日期
        /// </summary>
        public DateTime? endDate { get; set; } = DateTime.Today;
    }
}