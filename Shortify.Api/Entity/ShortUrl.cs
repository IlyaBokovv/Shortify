namespace Shortify.Api.Entity
{
    public class ShortUrl
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortedUrl { get; set; } = string.Empty;
        public string? Code { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
