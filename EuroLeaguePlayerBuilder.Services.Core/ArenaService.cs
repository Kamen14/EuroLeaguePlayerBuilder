using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.Services.Core
{
    public class ArenaService : IArenaService
    {
        private readonly IArenaRepository _arenaRepository;
        //private readonly IWebHostEnvironment

        public ArenaService(IArenaRepository arenaRepository)
        {
            _arenaRepository = arenaRepository;
        }


        public async Task<IEnumerable<ArenaDto>> GetAllArenasOrderedByNameAsync()
        {
            IEnumerable<ArenaDto> arenas = await _arenaRepository.GetAllArenasNoTracking()
                .Select(a => new ArenaDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    City = a.City,
                    Country = a.Country,
                    Capacity = a.Capacity,
                    ImagePath = a.ImagePath,
                    UserId = a.UserId
                })
                .OrderBy(a => a.Name)
                .ToListAsync();

            return arenas;
        }

        public async Task CreateArenaAsync(ArenaInputDto inputDto, string userId, string wwwRootPath)
        {
            string? imagePath = null;

            if (inputDto.Image != null && inputDto.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(wwwRootPath, "images", "arenas");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + inputDto.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await inputDto.Image.CopyToAsync(fileStream);
                }

                imagePath = "/images/arenas/" + uniqueFileName;
            }

            Arena arena = new Arena
            {
                Name = inputDto.Name,
                City = inputDto.City,
                Country = inputDto.Country,
                Capacity = inputDto.Capacity,
                ImagePath = imagePath,
                UserId = userId
            };

            bool successfulyAdded = await _arenaRepository.AddArenaAsync(arena);

            if (!successfulyAdded)
            {
                throw new DbUpdateException("An error occurred while adding the arena to the database.");
            }
        }

        public async Task<IEnumerable<ArenaDto>> GetUserArenas(string userId)
        {
            return await _arenaRepository
                .GetAllArenasNoTracking()
                .Where(a => a.UserId == userId)
                .Select(a => new ArenaDto 
                {
                    Id = a.Id,
                    Name = a.Name,
                    City = a.City,
                    Country = a.Country,
                    Capacity = a.Capacity,
                    ImagePath = a.ImagePath,
                    UserId = a.UserId
                })
                .OrderBy(aDto => aDto.Name)
                .ToListAsync();
        }
    }
}
