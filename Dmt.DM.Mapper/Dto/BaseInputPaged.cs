namespace Dmt.DM.Mapper.Dto
{
    public class BaseInputPaged
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
        /// 过滤描述
        /// </summary>
        public string keyValue { get; set; }
    }
}