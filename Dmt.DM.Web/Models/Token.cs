using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Web.Models
{
    public class Token
    {
        //[JsonPropertyName("refreshToken")]
        [Required]
        public string RefreshToken { get; set; }
    }
}
