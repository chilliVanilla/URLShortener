// using System.Data.Entity;

using Microsoft.EntityFrameworkCore;
using URLShortener.Data;
using URLShortener.DTOs;
using URLShortener.Interfaces;
using URLShortener.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace URLShortener.Services;

public class ShortLinkService:IShortLinkService
{
    private readonly IUrlValidator _urlValidator;
    private readonly ICodeGenerator _codeGenerator;
    private readonly ApplicationDbContext _dbContext;
    private const string InvalidUrlMessage = "This url link is invalid - Invalid Url";

    public ShortLinkService(IUrlValidator urlValidator, ICodeGenerator codeGenerator, ApplicationDbContext dbContext)
    {
        _urlValidator = urlValidator;
        _codeGenerator = codeGenerator;
        _dbContext = dbContext;
    }


    public async Task<ShortLinkResponseDto> CreateShortLinkAsync(string url, CancellationToken ct)
    {
        // First of all validate the url
        if (!_urlValidator.IsValidUrl(url))
        {
            throw new ArgumentException(InvalidUrlMessage);
        }
        
        //The loop to handle the retrials
        const int maxRetrialCount = 5;
        
        for (int attempt = 0; attempt < maxRetrialCount; attempt++)
        {
            Console.WriteLine("Attempt number: " + attempt);
            //Does stuff
            ct.ThrowIfCancellationRequested();
            
            //Now generate the 6 or 7 character code
            var code = _codeGenerator.Generate();
        
            //Create the short link
            var link = ShortUrlBuilder(code);
            
            //Now try saving this to the database 
            //Should have a maximum of 5 attempts before returning an error,
            //that is if the code has been taken, even up to the 5th code generated
            var shortLinkEntity = new ShortLink
            {
                Code = code,
                ClickCount = 0,
                OriginalUrl = url
            };
            try
            {
                //To stage and save to the database asynchronously
                _dbContext.ShortLinks.Add(shortLinkEntity);
                await _dbContext.SaveChangesAsync(ct);
                return new ShortLinkResponseDto (shortLinkEntity, link);
            }
            catch (Exception e)
            {
                _dbContext.Entry(shortLinkEntity).State = EntityState.Detached;
                if (attempt == maxRetrialCount)
                {
                    throw new InvalidOperationException("Could not create the short link - Max number of retrials!!!");
                }
            }
        }
        throw new InvalidOperationException("Could not create the short link - General Error!!!");


    }

    private Uri ShortUrlBuilder(string code)
    {
        var builder = new UriBuilder
        {
            Scheme = Uri.UriSchemeHttps,
            Host = "Klein.ln",
            Path = code
        };
        Uri uri = builder.Uri;
        return uri;
    }

    public async Task<string?> GetOriginalUrlAsync(string code, CancellationToken ct)
    {
        Console.WriteLine("In the desired method");
        if (string.IsNullOrWhiteSpace(code))
        {
            Console.WriteLine("Code was null???");
            return null;
        }

        var link = await _dbContext.ShortLinks
            .FirstOrDefaultAsync(x => x.Code == code, ct);
        if (link is null)
        {
            Console.WriteLine("The link entity is null in the GetOriginalUrlAsync method!!!");
            return null;
        }
        return link.OriginalUrl;
    }
    
}