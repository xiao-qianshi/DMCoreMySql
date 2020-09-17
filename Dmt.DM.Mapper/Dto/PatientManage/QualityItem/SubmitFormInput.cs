using System.Collections.Generic;

namespace Dmt.DM.Mapper.Dto.PatientManage.QualityItem
{
    public class SubmitFormInput
    {
        public string KeyValue { get; set; }
        public QualityItemDto Entity { get; set; }
        public List<PartitionDto> Partitions { get; set; }
    }
}
