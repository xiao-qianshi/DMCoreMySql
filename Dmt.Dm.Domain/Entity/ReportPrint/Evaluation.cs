using System;
using System.Collections.Generic;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Domain.Entity.ReportPrint.Evaluation
{
    public class Category
    {
        public string Name { get; set; }
        public string Dept { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public int? No { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<EvaluationEntity> Evaluations { get; set; }
    }
}
