using System;

namespace Dmt.DM.Mapper.Dto.Billing
{
    public class BillReportInput
    {
        public string PatientId { get; set; }
        public string Keyword { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 1   药品；2    耗材；4    诊疗；8    全部；（3，5，6，7）
        /// </summary>
        public int ClassType { get; set; }
        /// <summary>
        /// 1   已结账；2   未结账；4   全部；（3）
        /// </summary>
        public int StatusType { get; set; }
    }
}
