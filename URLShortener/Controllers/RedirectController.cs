using Microsoft.AspNetCore.Mvc;
using URLShortener.DTOs;
using URLShortener.Interfaces;
// using URLShortener.Models;
// using URLShortener.Services;

namespace URLShortener.Controllers;


[ApiController]
public class RedirectController:ControllerBase
{
    // private readonly ICodeGenerator _codeGenerator;
    // private readonly IUrlValidator _urlValidator;
    private readonly IShortLinkService _shortLinkService; 

    public RedirectController(
        IShortLinkService shortLinkService)
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

    // [HttpGet("Code_Generator")]
    // public IActionResult CodeGenerator()
    // {
    //     var code = _codeGenerator.Generate();
    //     Console.WriteLine("The generated code:" + code);
    //     return Ok(code);
    // }

    // [HttpGet("Valid Url Checker")]
    // public IActionResult UrlBoolTest(string? link)
    // {
    //     var result = _urlValidator.IsValidUrl(link);
    //     if (result == true)
    //     {
    //         return Ok("Url '" + link + "' is valid !!!");
    //     }
    //
    //     return BadRequest("Url is not valid!!!");
    // }
}