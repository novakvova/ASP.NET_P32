using Core.Models.Location;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCountries()
    {
        var countries = new List<CountryItemModel>
        {
            new CountryItemModel 
            { 
                Id = 1, 
                Name = "United States", 
                Code = "US", 
                Slug = "united-states", 
                Image = null 
            },
            new CountryItemModel 
            { 
                Id = 2, 
                Name = "Canada", 
                Code = "CA", 
                Slug = "canada", 
                Image = null 
            },
            // Additional countries can be added here.
        };
        // Implementation to retrieve and return countries would go here.
        return Ok(countries); //код 200
    }
}
