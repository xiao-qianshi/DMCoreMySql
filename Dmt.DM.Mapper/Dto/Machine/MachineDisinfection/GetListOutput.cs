using System;
using System.Collections.Generic;
using System.Linq;

namespace Dmt.Dm.Domain.Dto.Machine.MachineDisinfection
{
    public class GetListOutput
    {
        public DateTime visitDate { get; set; } = DateTime.Today;
        public int visitNo { get; set; } = 1;   //班次(1全部，2第一班，4第二班，8第三班）
        public string groupNames { get; set; }
        public string bedNo { get; set; }
        public int completeCount
        {
            get
            {
                return items.Count(t => t.beDisinfected == true);
            }
        }
        public int inCompleteCount
        {
            get
            {
                return items.Count(t => t.beDisinfected == false);
            }
        }
        public List<DisinfectionInfo> items { get; set; }
        public GetListOutput()
        {
            items = new List<DisinfectionInfo>();
        }
    }

    public class DisinfectionInfo
    {
        public string id { get; set; }
        public string vId { get; set; }
        public string groupName { get; set; }
        public string bedNo { get; set; }
        public bool beDisinfected { get; set; } = false;
        public int visitNo { get; set; }
        public string patientName { get; set; }
        public DateTime? dialysisStartTime { get; set; }
        public DateTime? dialysisEndTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }    
        public bool? Option1 { get; set; }
        public string Option1Value { get; set; }
        public bool? Option2 { get; set; }
        public string Option2Value { get; set; }
        public bool? Option3 { get; set; }
        public bool? Option4 { get; set; }
        public bool? Option5 { get; set; }
        public string OperatePerson { get; set; }
        public DateTime? WipeStartTime { get; set; }
        public DateTime? WipeEndTime { get; set; }
        public bool? Option6 { get; set; }
        public string Option6Value { get; set; }   
        public string CheckPerson { get; set; }
    }
}