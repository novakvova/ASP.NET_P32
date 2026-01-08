using Core.Interfaces;
using Core.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CartsController(ICartService cartService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await cartService.GetListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdate([FromBody] CartAddUpdateModel model)
        {
            await cartService.AddUpdateAsync(model);
            return Ok();
        }
    }
}
