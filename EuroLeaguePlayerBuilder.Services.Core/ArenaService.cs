using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using EuroLeaguePlayerBuilder.Services.Models.Arenas;
using EuroLeaguePlayerBuilder.Services.Models.Players;
using Microsoft.EntityFrameworkCore;
using static EuroLeaguePlayerBuilder.GCommon.ImageConstants.ArenaImages;
using static EuroLeaguePlayerBuilder.GCommon.ImageValidator;

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
                if (!await IsValidImageAsync(inputDto.Image))
                    throw new InvalidOperationException("Invalid file type.");

                const long MaxFileSize = MaxArenaImageSize;

                if (inputDto.Image.Length > MaxFileSize)
                    throw new InvalidOperationException("File size must not exceed 3MB.");

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

        public async Task<ArenaInputDto> GetArenaInputModelWithLoadedDataAsync(int id)
        {
            Arena? arena = await _arenaRepository
                .GetArenaByIdNoTrackingAsync(id);

            if(arena == null)
            {
                return null;
            }

            ArenaInputDto inputDto = new ArenaInputDto
            {
                Name = arena.Name,
                City = arena.City,
                Country = arena.Country,
                Capacity = arena.Capacity,
                ImagePath = arena.ImagePath
            };

            return inputDto;
        }

        public async Task<bool> IsArenaOwnedByUserAsync(int arenaId, string userId)
        {
            string? arenaUserId = await _arenaRepository
                .GetAllArenasNoTracking()
                .Where(a => a.Id == arenaId)
                .Select(a => a.UserId)
                .SingleOrDefaultAsync();

            return arenaUserId != null && arenaUserId == userId;
        }

        public async Task<bool> ArenaExistsAsync(int id)
        {
            bool selectedArenaExists = await _arenaRepository
                .GetAllArenasNoTracking()
                .AnyAsync(p => p.Id == id);

            return selectedArenaExists;
        }

        public async Task EditArenaAsync(int id, ArenaInputDto inputDto, string wwwRootPath)
        {
            Arena? selectedArena = await _arenaRepository
                .GetAllArenas()
                .SingleOrDefaultAsync(a => a.Id == id);

            if (selectedArena == null)
            {
                throw new ArgumentException("Arena with the provided ID does not exist.");
            }

            selectedArena.Name = inputDto.Name;
            selectedArena.City = inputDto.City;
            selectedArena.Country = inputDto.Country;
            selectedArena.Capacity = inputDto.Capacity;

            if (inputDto.Image != null && inputDto.Image.Length > 0)
            {
                // Validate file type and size

                if (!await IsValidImageAsync(inputDto.Image))
                    throw new InvalidOperationException("Invalid file type.");

                const long MaxFileSize = MaxArenaImageSize;

                if (inputDto.Image.Length > MaxFileSize)
                    throw new InvalidOperationException("File size must not exceed 3MB.");

                string uploadsFolder = Path.Combine(wwwRootPath, "images", "arenas");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + inputDto.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await inputDto.Image.CopyToAsync(fileStream);
                }

                selectedArena.ImagePath = "/images/arenas/" + uniqueFileName;
            }

            await _arenaRepository.UpdateArenaAsync(selectedArena);
        }

        public async Task<DeleteArenaDto> GetArenaForDeleteByIdAsync(int id)
        {
            Arena? arena = await _arenaRepository
                .GetAllArenas()
               .SingleOrDefaultAsync(a => a.Id == id);

            if (arena == null)
            {
                return null;
            }

            DeleteArenaDto deleteDto = new DeleteArenaDto
            {
                Name = arena.Name,
                City = arena.City
            };

            return deleteDto;
        }

        public async Task DeleteArenaAsync(int id)
        {
            Arena? selectedArena = await _arenaRepository
               .GetAllArenas()
               .SingleOrDefaultAsync(a => a.Id == id);

            if (selectedArena == null)
            {
                throw new ArgumentException("Player with the provided ID does not exist.");
            }

            await _arenaRepository.DeleteArenaAsync(selectedArena);
        }
    }
}
