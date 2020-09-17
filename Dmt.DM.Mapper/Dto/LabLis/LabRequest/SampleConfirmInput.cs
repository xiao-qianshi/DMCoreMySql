using System;

namespace Dmt.DM.Mapper.Dto.LabLis.LabRequest
{
    public class SampleConfirmInput
    {
        public string Barcode { get; set; }
        public DateTime? SamplingTime { get; set; }
    }
}
