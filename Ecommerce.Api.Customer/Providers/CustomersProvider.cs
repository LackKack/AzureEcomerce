using AutoMapper;
using Ecommerce.Api.Customer.Db;
using Ecommerce.Api.Customer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customer.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly ILogger<CustomersProvider> logger;
        private readonly CustomerDbContext dbContext;
        private readonly IMapper mapper;

        public CustomersProvider(ILogger<CustomersProvider> logger
            , CustomerDbContext dbContext
            , IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;

            SeekData();
        }

        private void SeekData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer { Id = 1, Name = "Kyle", Email = "l.k@e.com" });
                dbContext.Customers.Add(new Db.Customer { Id = 2, Name = "ThaoTran", Email = "thao.tran@e.com" });
                dbContext.Customers.Add(new Db.Customer { Id = 3, Name = "Leshin", Email = "leshin@e.com" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customers = await dbContext.Customers.FirstOrDefaultAsync(e => e.Id == id);
                if (customers != null)
                {
                    var result = mapper.Map<Models.Customer>(customers);
                    return (true, result, string.Empty);
                }
                else
                {
                    return (false, null, "Not found");
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null)
                {
                    var result = mapper.Map<IEnumerable<Models.Customer>>(customers);
                    return (true, result, string.Empty);
                }
                else
                {
                    return (false, null, "Not found");
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
