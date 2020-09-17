using System;

namespace Dmt.DM.Web.SignalR
{
    public class NotifyModel
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Content { get; set; }
        public NotifyType Type { get; set; } = NotifyType.Info;
    }

    public enum NotifyType
    {
        Info,
        Warning,
        Danger
    }
}
