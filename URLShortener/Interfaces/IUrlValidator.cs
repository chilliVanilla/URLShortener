namespace URLShortener.Interfaces;

public interface IUrlValidator
{
    public bool IsValidUrl(string link);
}