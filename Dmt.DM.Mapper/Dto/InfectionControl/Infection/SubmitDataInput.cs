using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dmt.Dm.Domain.Dto.InfectionControl.Infection
{
    public class SubmitDataInput
    {
        public string id { get; set; }
        public DateTime? reportDate { get; set; } = DateTime.Now;
        public float? item1 { get; set; }
        public float? item2 { get; set; }
        public float? item3 { get; set; }
        public float? item4 { get; set; }
        public float? item5 { get; set; }
        public float? item6 { get; set; }
        public float? item7 { get; set; }
        //public float? item1 { get; set; }
    }
}