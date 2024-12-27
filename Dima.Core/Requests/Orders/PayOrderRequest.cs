using Dima.Core.Models;

namespace Dima.Core.Requests.Orders
{
    public class PayOrderRequest : BaseRequest<Order>
    {
        public long Id { get; set; }
        public string? ExternalReference { get; set; }
    }
}
