using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint
{
    public class WaterMLog
    {
        public string HospialName { get; set; }
        public byte[] HospialLogo { get; set; }
        public string PrintDate { get; set; }
        //public float Costs { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<WaterMLogPrint> Items { get; set; }
        public WaterMLog()
        {
            Items = new List<WaterMLogPrint>();
        }
    }

    public class WaterMLogPrint
    {
        public string Id { get; set; }
        public string LogDate { get; set; }
        /// <summary>
        /// 进水压力
        /// </summary>
        public string Value1 { get; set; }
        /// <summary>
        /// 2级膜产水压
        /// </summary>
        public string Value2 { get; set; }
        /// <summary>
        /// 硬度
        /// </summary>
        public string Value3 { get; set; }
        /// <summary>
        /// 残余氯
        /// </summary>
        public string Value4 { get; set; }
        /// <summary>
        /// 水温
        /// </summary>
        public string Value5 { get; set; }
        /// <summary>
        /// RO水电导
        /// </summary>
        public string Value6 { get; set; }
        /// <summary>
        /// RO水产水压
        /// </summary>
        public string Value7 { get; set; }
        /// <summary>
        /// RO回水压力
        /// </summary>
        public string Value8 { get; set; }
        /// <summary>
        /// PH值
        /// </summary>
        public string Value9 { get; set; }
        /// <summary>
        /// 石英砂罐反冲
        /// </summary>
        public string Option1 { get; set; }
        /// <summary>
        /// 活性炭罐反冲
        /// </summary>
        public string Option2 { get; set; }
        /// <summary>
        /// 活性炭罐反冲
        /// </summary>
        public string Option3 { get; set; }
        /// <summary>
        /// 树脂罐反冲
        /// </summary>
        public string Option4 { get; set; }
        /// <summary>
        /// 系统消毒
        /// </summary>
        public string Option5 { get; set; }
        /// <summary>
        /// 系统状况
        /// </summary>
        public string Option6 { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Memo { get; set; }
        public string OperatePerson { get; set; }
        public string CheckPerson { get; set; }
    }
}
