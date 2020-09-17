using System;
using System.Collections.Generic;

namespace Dmt.DM.Mapper.Dto.StorageManage
{
    public class ImportDto
    {
        public string KeyValue { get; set; }
        public Master Master { get; set; }
        public List<Detail> Details { get; set; }
    }

    public class Master
    {
        public string F_ImpClass { get; set; }
        public string F_ImpType { get; set; }
        public DateTime? F_ImpDate { get; set; }
        public string F_ImpNo { get; set; }
        public string F_Storage { get; set; }
        public float? F_Costs { get; set; }
        public string F_Supplier { get; set; }
        public string F_Contacts { get; set; }
        public string F_SupplierPhone { get; set; }
    }

    public class Detail
    {
        public string F_Id { get; set; }
        public int? F_Amount { get; set; }
        public string F_ItemId { get; set; }
    }
}
