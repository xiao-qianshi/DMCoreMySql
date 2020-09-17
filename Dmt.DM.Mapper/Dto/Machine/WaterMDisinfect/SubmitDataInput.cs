using System;

namespace Dmt.Dm.Domain.Dto.Machine.WaterMDisinfect
{
    public class SubmitDataInput
    {
        public string id { get; set; }
        public DateTime disinfectDate { get; set; } = DateTime.Today;
        public string disinfectantName { get; set; }
        public float? disinfectantVolume { get; set; }
        public string disinfectantUnit { get; set; }
        public string disinfectType { get; set; }
        public bool? option1 { get; set; } = false;
        public DateTime? recyclingStartTime { get; set; }
        public DateTime? recyclingEndTime { get; set; }
        public float? recyclingMinutes { get; set; }
        public DateTime? soakStartTime { get; set; }
        public DateTime? soakEndTime { get; set; }
        public float? soakMinutes { get; set; }
        public DateTime? rinseStartTime { get; set; }
        public DateTime? rinseEndTime { get; set; }
        public float? rinseMinutes { get; set; }
        public bool? option2 { get; set; } = false;
        public bool? option3 { get; set; } = false;
    }
}