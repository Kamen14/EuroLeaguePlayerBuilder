using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EuroLeaguePlayerBuilder.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        //Team One (not necesserily the home team)
        [Required]
        public int TeamOneId { get; set; }

        public Team TeamOne { get; set; } = null!;

        [Required]
        public int TeamOneScore { get; set; }

        //Team Two (not necessarily the away team)
        [Required]
        public int TeamTwoId { get; set; }

        public Team TeamTwo { get; set; } = null!;

        [Required]
        public int TeamTwoScore { get; set; }

        [Required]
        public int ArenaId { get; set; }

        public Arena Arena { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;
    }
}
