using Microsoft.AspNetCore.Mvc;
using URLShortener.DTOs;
using URLShortener.Interfaces;

namespace URLShortener.Controllers;


[ApiController]
public class RedirectController:ControllerBase
{
    // private readonly ICodeGenerator _codeGenerator;
    // private readonly IUrlValidator _urlValidator;
    private readonly IShortLinkService _shortLinkService; 

    public RedirectController(IShortLinkService shortLinkService)
    {
        // _codeGenerator = codeGenerator;
        // _urlValidator = urlValidator;
        _shortLinkService = shortLinkService;
    }
    
    // [HttpGet("/{code}")]
    [HttpPost("/Create")]
    public async Task<IActionResult> Create(string longUrl, CancellationToken ct)
    {
        try
        {
            ShortLinkResponseDto shortLinkEntityDto = await _shortLinkService.CreateShortLinkAsync(longUrl, ct);
            return CreatedAtAction(nameof(Create), new { code = shortLinkEntityDto.ShortLinkEntity.Code },
                shortLinkEntityDto.ShortLink);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    [HttpGet("/Long Url")]
    public async Task<IActionResult> Get(string code, CancellationToken ct)
    {
        var link = await _shortLinkService.GetOriginalUrlAsync(code, ct);

        if (link is null)
        {
            return BadRequest("Code does not exist!!!");
        }
        //This returns a 301 - which is a permanent redirect
        return RedirectPermanent(link);
        
        //Redirect(link) returns a 302, which is a temporary redirect
    }
}