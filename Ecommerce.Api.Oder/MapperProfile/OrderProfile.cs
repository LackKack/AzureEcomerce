namespace Ecommerce.Api.Oder.MapperProfile
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, Models.Order>();
            CreateMap<Db.OrderDetail, Models.OrderDetail>();
        }
    }
}
