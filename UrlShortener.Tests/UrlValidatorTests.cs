namespace UrlShortener.Tests;

using URLShortener.Services;
using Xunit;


public class UrlValidatorTests
{
    private UrlValidator _validator = new();

    [Theory]
    [InlineData("https://example.com/path?x=1", true)]
    [InlineData("example.com", false)]
    [InlineData("javascript:alert(1)", false)]
    [InlineData("ftp://example.com", false)]
    [InlineData("", false)]

    public void IsValidUrl_ReturnsExpectedResult(string? link, bool expected)
    {
        var result = _validator.IsValidUrl(link);
        Assert.Equal(expected, result);
    }
}