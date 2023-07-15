using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Oder.Db
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions dbContext) : base(dbContext)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
