using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint
{
    public class StorageCategory
    {
        public string HospialName { get; set; }
        public byte[] HospialLogo { get; set; }
        public string PrintDate { get; set; }
        public float Costs { get; set; }
        public string StartDate { get; set; }
        public List<StorageSummary> StorageSummaries { get; set; }
        public StorageCategory()
        {
            StorageSummaries = new List<StorageSummary>();
        }
    }

    public class StorageSummary
    {
        public string StorageType { get; set; }
        public float Costs { get; set; }
        public List<StorageDetail> StorageDetails { get; set; }
        public StorageSummary()
        {
            StorageDetails = new List<StorageDetail>();
        }
    }

    public class StorageDetail
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemSpec { get; set; }
        public float Amount { get; set; }
        public float? RealAmount { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }
        public float Charges { get; set; }
        public string CheckResultType { get; set; }
        public string ResultAmount { get; set; }

    }
}
