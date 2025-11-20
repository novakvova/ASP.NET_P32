using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCountries()
    {
        
        // Implementation to retrieve and return countries would go here.
        return Ok();
    }
}
