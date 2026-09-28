using URLShortener.Interfaces;

namespace URLShortener.Services;

public class UrlValidator : IUrlValidator
{
    public bool IsValidUrl(string? link)
    {
        if (string.IsNullOrWhiteSpace(link) || link.Length > 2048)
        {
            return false;
        }
        return Uri.TryCreate(link, UriKind.Absolute, out Uri? uriResult) 
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}