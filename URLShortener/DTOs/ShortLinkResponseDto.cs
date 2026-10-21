using URLShortener.Models;

namespace URLShortener.DTOs;

//This DTO is to help format the response from the _shortLinkService.CreateShortLinkAsync(longUrl, ct) call
//So that I can have both the ShortLinkEntity and also the actual short link since it won't be saved to the database
public record ShortLinkResponseDto(
    ShortLink ShortLinkEntity, Uri ShortLink 
    );