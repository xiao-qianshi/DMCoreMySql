using System.Collections.Generic;

namespace Dmt.DM.Domain.Entity.ReportPrint
{
    public class FeeCategory
    {
        public string HospialName { get; set; }
        public byte[] HospialLogo { get; set; }
        public string Name { get; set; }
        public string DialysisNo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RecordNo { get; set; }
        public string PatientNo { get; set; }
        public string Gender { get; set; }
        public string AgeString { get; set; }
        public string BirthDay { get; set; }
        public string InsuranceNo { get; set; }
        public string IdNo { get; set; }
        public float Costs { get; set; }
        public List<BillSummary> BillSummaries { get; set; }
        public FeeCategory()
        {
            BillSummaries = new List<BillSummary>();
        }
    }

    public class BillSummary
    {
        public string FeeType { get; set; }
        public float Costs { get; set; }
        public List<BillDetail> BillDetails { get; set; }
        public BillSummary()
        {
            BillDetails = new List<BillDetail>();
        }
    }

    public class BillDetail
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemSpec { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }
        public float Charges { get; set; }
        public float Costs { get; set; }
    }
}
