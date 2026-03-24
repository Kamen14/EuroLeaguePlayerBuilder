using EuroLeaguePlayerBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EuroLeaguePlayerBuilder.GCommon.ImageConstants.ArenaImages;

namespace EuroLeaguePlayerBuilder.Data.Configuration
{
    public class ArenaEntityConfiguration : IEntityTypeConfiguration<Arena>
    {
        private readonly IEnumerable<Arena> Arenas = new List<Arena>()
        {
            new Arena { Id = 1, Name = "OAKA", City = "Athens", Country = "Greece", Capacity = 19443, ImagePath = OakaImage },
            new Arena { Id = 2, Name = "Peace and Friendship Stadium", City = "Piraeus", Country = "Greece", Capacity = 12000, ImagePath = PeaceAndFriendshipStadiumImage},
            new Arena { Id = 3, Name = "WiZink Center", City = "Madrid", Country = "Spain", Capacity = 15000, ImagePath = WiZinkCenterImage},
            new Arena { Id = 4, Name = "Palau Blaugrana", City = "Barcelona", Country = "Spain", Capacity = 7585, ImagePath = PalauBlaugranaImage},
            new Arena { Id = 5, Name = "Belgrade Arena", City = "Belgrade", Country = "Serbia", Capacity = 18386, ImagePath = BelgradeArenaImage},
            
        };
        public void Configure(EntityTypeBuilder<Arena> entity)
        {
            entity.HasData(Arenas);
        }
    }
}
