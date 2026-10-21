using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models;

public class ShortLink
{
    [Key]
    [MaxLength(7)]
    public required string Code { get; set; }
    public required string OriginalUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public int ClickCount { get; set; }
}