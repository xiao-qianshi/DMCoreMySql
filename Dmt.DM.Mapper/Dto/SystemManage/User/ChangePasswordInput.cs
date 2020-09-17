namespace Dmt.DM.Mapper.Dto.SystemManage.User
{
    public class ChangePasswordInput
    {
        public string OldUserPassword { get; set; }
        public string UserPassword { get; set; }
        public string KeyValue { get; set; }
    }
}
