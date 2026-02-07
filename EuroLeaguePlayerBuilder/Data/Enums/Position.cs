using System.ComponentModel.DataAnnotations;

namespace EuroLeaguePlayerBuilder.Data.Enums
{
    public enum Position
    {
        [Display(Name = "Point Guard")]
        PointGuard,

        [Display(Name = "Shooting Guard")]
        ShootingGuard,

        [Display(Name = "Small Forward")]
        SmallForward,

        [Display(Name = "Power Forward")]
        PowerForward,

        [Display(Name = "Center")]
        Center
    }
}
