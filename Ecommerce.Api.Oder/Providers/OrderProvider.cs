using AutoMapper;
using Ecommerce.Api.Oder.Db;
using Ecommerce.Api.Oder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Oder.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrderProvider> logger;
        private readonly IMapper mapper;

        public OrderProvider(OrderDbContext dbContext, ILogger<OrderProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeekData();
        }

        private void SeekData()
        {
            if (dbContext.Orders != null && !dbContext.Orders.Any())
            {
                dbContext.Orders.Add(
                    new Db.Order
                    {
                        Id = 1,
                        CustomerId = 1
                        ,
                        OrderDate = DateTime.Now
                        ,
                        Total = 12000
                        ,
                        Items = new List<Db.OrderDetail>()
                        {
                            new Db.OrderDetail
                            {
                                Id=1,
                                OrderId=1,
                                ProductId=1,
                                Quantity=1,
                                UnitPrice=10
                            },
                            new Db.OrderDetail
                            {
                                Id=2,
                                OrderId=1,
                                ProductId=2,
                                Quantity=2,
                                UnitPrice=40
                            },
                        }
                    });
                dbContext.Orders.Add(
                    new Db.Order
                    {
                        Id = 2,
                        CustomerId = 2
                        ,
                        OrderDate = DateTime.Now
                        ,
                        Total = 12000
                        ,
                        Items = new List<Db.OrderDetail>()
                        {
                            new Db.OrderDetail
                            {
                                Id=3,
                                OrderId=2,
                                ProductId=3,
                                Quantity=1,
                                UnitPrice=10
                            },
                            new Db.OrderDetail
                            {
                                Id=4,
                                OrderId=2,
                                ProductId=4,
                                Quantity=2,
                                UnitPrice=40
                            },
                        }
                    });
                dbContext.Orders.Add(
                    new Db.Order
                    {
                        Id = 3,
                        CustomerId = 3
                        ,
                        OrderDate = DateTime.Now
                        ,
                        Total = 12000
                        ,
                        Items = new List<Db.OrderDetail>()
                        {
                            new Db.OrderDetail
                            {
                                Id=5,
                                OrderId=3,
                                ProductId=2,
                                Quantity=1,
                                UnitPrice=15
                            },
                            new Db.OrderDetail
                            {
                                Id=6,
                                OrderId=3,
                                ProductId=1,
                                Quantity=4,
                                UnitPrice=30
                            },
                        }
                    });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOdersAsync(int customerId)
        {
            try
            {
                var orders = dbContext.Orders
                     .Where(e => e.CustomerId == customerId)
                     .Include(e => e.Items);
                if (await orders.AnyAsync())
                {
                    var result = mapper.Map<IEnumerable<Models.Order>>(await orders.ToListAsync());
                    return (true, result, string.Empty);
                }
                else
                {
                    return (false, null, "Not found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
