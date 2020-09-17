namespace Dmt.DM.Web.Models
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AppSettingOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public App App { get; set; }
        public Authentication Authentication { get; set; }
        public ASFFace ASFFace { get; set; }
    }

    public class ConnectionStrings
    {
        public string Default { get; set; }
    }

    public class App
    {
        public string ServerRootAddress { get; set; }
        public string ClientRootAddress { get; set; }
        public string CorsOrigins { get; set; }
    }

    public class Authentication
    {
        public JwtBearer JwtBearer { get; set; }
    }

    public class JwtBearer
    {
        public string IsEnabled { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class ASFFace
    {
        public string App_Id { get; set; }
        public string SdkKey64 { get; set; }
    }
}
