using Ecommerce.Api.Oder.Models;

namespace Ecommerce.Api.Oder.Interfaces
{
    public interface IOrderProvider
    {
        public Task<(bool IsSuccess, IEnumerable<Order> orders, string ErrorMessage)> GetOdersAsync(int customerId);
    }
}
