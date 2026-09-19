namespace URLShortener.Models;

public class ShortLink
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required string OriginalUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public int ClickCount { get; set; }
}