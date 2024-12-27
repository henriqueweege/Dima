using Dima.Core.Models;

namespace Dima.Core.Requests.Orders
{
    public class CreateOrderRequest : BaseRequest<Order>
    {
        public long ProductId { get; set; }
        public long? VoucherId { get; set; }
    }
}
