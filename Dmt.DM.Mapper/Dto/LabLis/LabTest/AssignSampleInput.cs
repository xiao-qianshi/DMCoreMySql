using System;

namespace Dmt.DM.Mapper.Dto.LabLis.LabTest
{
    public class AssignSampleInput
    {
        public string InstrumentId { get; set; }
        public DateTime? TestDate { get; set; }
        public string Barcode { get; set; }
    }
}
