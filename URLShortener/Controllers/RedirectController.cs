using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers;


[ApiController]
public class RedirectController:ControllerBase
{
    [HttpGet("/{code}")]
    public IActionResult Get(string code)
    {
        return Ok(code);
    }
}