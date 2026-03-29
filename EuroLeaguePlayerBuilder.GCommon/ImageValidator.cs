using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroLeaguePlayerBuilder.GCommon
{
    public static class ImageValidator
    {
        private static readonly Dictionary<string, byte[]> ImageMagicNumbers = new()
        {
            { "image/jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { "image/png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
            { "image/webp", new byte[] { 0x52, 0x49, 0x46, 0x46 } },
            { "image/gif", new byte[] { 0x47, 0x49, 0x46 } }
        };

        public static async Task<bool> IsValidImageAsync(IFormFile file)
        {
            byte[] buffer = new byte[4];
            using var stream = file.OpenReadStream();
            await stream.ReadAsync(buffer, 0, 4);

            return ImageMagicNumbers.Values.Any(magic =>
                buffer.Take(magic.Length).SequenceEqual(magic));
        }
    }
}
