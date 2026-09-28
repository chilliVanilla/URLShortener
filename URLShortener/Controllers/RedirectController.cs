using Microsoft.AspNetCore.Mvc;
using URLShortener.Interfaces;
using URLShortener.Services;

namespace URLShortener.Controllers;


[ApiController]
public class RedirectController:ControllerBase
{
    private readonly ICodeGenerator _codeGenerator;
    private readonly IUrlValidator _urlValidator;

    public RedirectController(ICodeGenerator codeGenerator, IUrlValidator urlValidator)
    {
        _codeGenerator = codeGenerator;
        _urlValidator = urlValidator;
    }
    
    [HttpGet("/{code}")]
    public IActionResult Get(string code)
    {
        return Ok(code);
    }

    [HttpGet("Code_Generator")]
    public IActionResult CodeGenerator()
    {
        var code = _codeGenerator.Generate();
        Console.WriteLine("The generated code:" + code);
        return Ok(code);
    }

    [HttpGet("Valid Url Checker")]
    public IActionResult UrlBoolTest(string? link)
    {
        var result = _urlValidator.IsValidUrl(link);
        if (result == true)
        {
            return Ok("Url '" + link + "' is valid !!!");
        }

        return BadRequest("Url is not valid!!!");
    }
}