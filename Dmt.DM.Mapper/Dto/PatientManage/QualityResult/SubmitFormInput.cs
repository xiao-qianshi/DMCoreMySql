using System;
using System.Collections.Generic;

namespace Dmt.DM.Mapper.Dto.PatientManage.QualityResult
{
    public class SubmitFormInput
    {
        public string PatientId { get; set; }
        public List<ResultItem> Items { get; set; }
    }

    public class ResultItem
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string Result { get; set; }
        public DateTime? ReportTime { get; set; } = DateTime.Now;
        public string Memo { get; set; }
        public string Flag { get; set; }
    }
}
