using System.Collections.Generic;

namespace Dmt.DM.Mapper.Dto.Billing
{
    public class AddFeeInput
    {       
        public string PatientId { get; set; }
        public List<BillItem> Items { get; set; }
        public AddFeeInput()
        {
            Items = new List<BillItem>();
        }
    }
    public class BillItem
    {
        /// <summary>
        /// 1 药品；2  耗材；3    诊疗
        /// </summary>
        public int BillType { get; set; } = 0;
        public string ItemId { get; set; }
        public float Amount { get; set; }
    }
}