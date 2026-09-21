using Microsoft.AspNetCore.Mvc;
using URLShortener.Interfaces;
using URLShortener.Services;

namespace URLShortener.Controllers;


[ApiController]
public class RedirectController:ControllerBase
{
    private readonly ICodeGenerator _codeGenerator;

    public RedirectController(ICodeGenerator codeGenerator)
    {
        _codeGenerator = codeGenerator;
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
}