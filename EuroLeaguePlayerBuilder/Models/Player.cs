namespace EuroLeaguePlayerBuilder.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidation.Player;
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PlayerNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(PlayerNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(PlayerPositionMaxLength)]
        public string Position { get; set; } = null!;

        [Required]
        public double PointsPerGame { get; set; }

        [Required]
        public double ReboundsPerGame { get; set; }

        [Required]
        public double AssistsPerGame { get; set; }

        [Required]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; } = null!;
    }
}
