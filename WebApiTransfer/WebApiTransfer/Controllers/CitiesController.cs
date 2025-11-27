using Core.Interfaces;
using Core.Models.Location.City;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(ICityService cityService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var list = await cityService.GetListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CityCreateModel model)
        {
            var item = await cityService.CreateAsync(model);
            return Ok(item);
        }
    }
}
