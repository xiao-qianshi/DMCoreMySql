using System.Collections.Generic;
namespace Dmt.Dm.Domain.Dto.DialysisSchedule
{
    public class CreateVisitRecordInput
    {
        public List<string> items { get; set; }
        public CreateVisitRecordInput()
        {
            items = new List<string>();
        }
    }
}