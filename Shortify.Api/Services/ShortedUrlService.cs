using Microsoft.EntityFrameworkCore;

namespace Shortify.Api.Services
{
    public class ShortedUrlService
    {
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int URL_LENGHT = 7;

        private readonly Random _random = new Random();
        private readonly ApplicationDbContext _db;

        public ShortedUrlService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerateUniqueCode()
        {
            var chars = new char[URL_LENGHT];

            while (true)
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    var randomIndex = _random.Next(URL_LENGHT);

                    chars[i] = ALPHABET[randomIndex];
                }

                var code = new string(chars);

                if (!await _db.ShortUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }

        }
    }

}
