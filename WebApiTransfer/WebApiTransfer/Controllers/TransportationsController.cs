using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TransportationsController(ITransportationService ts) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await ts.GetListAsync();
        return Ok(result);
    }
}
