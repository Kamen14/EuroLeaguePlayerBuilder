namespace EuroLeaguePlayerBuilder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidation.Coach;
    public class Coach
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CoachNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(CoachNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public int TitlesWon { get; set; }
    }
}
