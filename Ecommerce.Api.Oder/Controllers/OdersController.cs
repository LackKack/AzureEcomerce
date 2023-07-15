using Ecommerce.Api.Oder.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Oder.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OdersController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OdersController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await orderProvider.GetOdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.orders);
            }
            return NotFound();
        }
    }
}
