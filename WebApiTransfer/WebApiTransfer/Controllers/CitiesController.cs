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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id)
        {
            var item = await cityService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var item = await cityService.DeleteAsync(id);
            return Ok(item);
        }
    }
}
