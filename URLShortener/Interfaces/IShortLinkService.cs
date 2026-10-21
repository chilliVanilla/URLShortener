using URLShortener.DTOs;
using URLShortener.Models;

namespace URLShortener.Interfaces;

public interface IShortLinkService
{

    public Task<ShortLinkResponseDto> CreateShortLinkAsync(string originalUrl, CancellationToken ct);
    public Task<string?> GetOriginalUrlAsync(string code, CancellationToken ct);
}