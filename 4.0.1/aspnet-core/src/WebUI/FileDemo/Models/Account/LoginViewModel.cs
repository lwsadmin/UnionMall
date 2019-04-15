using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace UnionMall.Web.Models.Account
{
    public class LoginViewModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "TenancyName is not empety")]
        public string TenancyName { get; set; }

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        [DisableAuditing]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
