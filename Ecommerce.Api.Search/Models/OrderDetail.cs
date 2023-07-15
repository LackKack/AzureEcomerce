namespace Ecommerce.Api.Search.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductNanme { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
