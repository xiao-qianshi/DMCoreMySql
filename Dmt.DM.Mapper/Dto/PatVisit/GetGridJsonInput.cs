using Dmt.DM.Code;

namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetGridJsonInput
    {
        public Pagination pagination { get; set; }
        public FilterSet filterSet { get; set; }
    }

    public class FilterSet
    {
        /// <summary>
        /// 李竹青
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bcall { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bc1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bc2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bc3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string statusall { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string statuscomplete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string statusuncomplete { get; set; }
    }
}