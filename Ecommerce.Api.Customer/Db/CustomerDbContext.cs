using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customer.Db
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
