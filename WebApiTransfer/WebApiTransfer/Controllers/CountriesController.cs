using Core.Interfaces;
using Core.Models.Location;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountryService countryService) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        var list = await countryService.GetListAsync();
        // Implementation to retrieve and return countries would go here.
        return Ok(list); //код 200
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromForm] CountryCreateModel model)
    {
        var item = await countryService.CreateAsync(model);

        return CreatedAtAction(null, item);
        //return Created(item); //код 201
    }
}
