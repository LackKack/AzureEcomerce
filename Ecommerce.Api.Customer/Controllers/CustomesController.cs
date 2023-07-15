using Ecommerce.Api.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Customer.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomesController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomesController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
