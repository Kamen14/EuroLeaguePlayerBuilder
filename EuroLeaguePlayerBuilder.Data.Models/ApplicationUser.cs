using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static EuroLeaguePlayerBuilder.GCommon.EntityValidation.ApplicationUser;

namespace EuroLeaguePlayerBuilder.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(NicknameMaxLength)]
        public string? Nickname { get; set; }
    }
}
