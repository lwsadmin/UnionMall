using System.ComponentModel.DataAnnotations;

namespace UnionMall.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}