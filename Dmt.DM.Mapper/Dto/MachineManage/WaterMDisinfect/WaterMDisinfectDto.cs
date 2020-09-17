using System;
using System.Collections.Generic;
using System.Text;

namespace Dmt.DM.Mapper.Dto.MachineManage.WaterMDisinfect
{
    public class WaterMDisinfectDto
    {
        public string F_DisinfectDate { get; set; }
        public string F_DisinfectantName { get; set; }
        public string F_DisinfectantVolume { get; set; }
        public string F_DisinfectantUnit { get; set; }
        public string F_DisinfectType { get; set; }
        public string F_Option1 { get; set; }
        public string F_RecyclingStartTime { get; set; }
        public string F_RecyclingEndTime { get; set; }
        public string F_RecyclingMinutes { get; set; }
        public string F_SoakStartTime { get; set; }
        public string F_SoakEndTime { get; set; }
        public string F_SoakMinutes { get; set; }
        public string F_RinseStartTime { get; set; }
        public string F_RinseEndTime { get; set; }
        public string F_RinseMinutes { get; set; }
        public string F_Option2 { get; set; }
        public string F_Option3 { get; set; }
        public string F_OperatePerson { get; set; }
        public string F_CheckPerson { get; set; }
    }
}
