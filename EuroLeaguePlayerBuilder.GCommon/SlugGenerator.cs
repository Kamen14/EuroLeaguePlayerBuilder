namespace EuroLeaguePlayerBuilder.GCommon
{
    public class SlugGenerator
    {
        public static string GenerateSlug(string input)
        {
            string[] elements = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower().Trim())
                .ToArray();

            string slug = string.Join('-', elements);

            return slug;
        }
    }
}
