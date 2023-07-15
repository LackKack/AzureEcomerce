using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomerService customerService;

        public SearchService(IOrdersService ordersService
            , IProductsService productsService
            , ICustomerService customerService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var orderResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductsAsync();
            var customerResult = await customerService.GetCustomerAsync(customerId);
            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var orderItem in order.Items)
                    {
                        orderItem.ProductNanme = productResult.IsSucess
                                                ? productResult.Products.FirstOrDefault(p => p.Id == orderItem.ProductId)?.Name
                                                : productResult.ErrorMessage;
                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer
                    : new { Name = "Customer information is not available" },
                    Orders = orderResult.Orders,
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
