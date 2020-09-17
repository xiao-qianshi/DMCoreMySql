using System;

namespace Dmt.DM.Mapper.Dto.PatientManage.Bill
{
    public class BillDto
    {
        public string F_Pid { get; set; }
        public string F_DialylisNo { get; set; }
        public string F_PName { get; set; }
        public string F_PGender { get; set; }
        public string F_BillingDateTime { get; set; }
        public string F_BillingPersonId { get; set; }
        public string F_BillingPerson { get; set; }
        public string F_ItemId { get; set; }
        public string F_ItemClass { get; set; }
        public string F_ItemCode { get; set; }
        public string F_ItemName { get; set; }
        public string F_ItemSpec { get; set; }
        public string F_ItemUnit { get; set; }
        public string F_Amount { get; set; }
        public string F_Supplier { get; set; }
        public string F_Charges { get; set; }
        public string F_Costs { get; set; }
        public string F_ItemSpell { get; set; }
        public string F_EnabledMark { get; set; }
        public string F_IsAcct { get; set; }
        public string F_DisabledPerson { get; set; }
    }
}
