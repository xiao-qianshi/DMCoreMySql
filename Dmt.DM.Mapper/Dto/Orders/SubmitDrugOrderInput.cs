using System;
using System.Collections.Generic;

namespace Dmt.DM.Mapper.Dto.Orders
{
    /// <summary>
    /// 新建医嘱 （包含复合医嘱）
    /// </summary>
    public class SubmitDrugOrderInput
    {
        public string Pid { get; set; }
        public bool IsTemporary { get; set; } = false;
        public DateTime? OrderStartTime { get; set; } = DateTime.Now;
        public string OrderFrequency { get; set; }
        public string OrderAdministration { get; set; }
        public List<DrugOrderItem> Items { get; set; } = new List<DrugOrderItem>();
    }

    public class DrugOrderItem
    {
        public string OrderCode { get; set; }
        public float? OrderAmount { get; set; }
    }
}
