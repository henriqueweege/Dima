using Dima.Core.Models;

namespace Dima.Core.Requests.Orders
{
    public class CancelOrderRequest : BaseRequest<Order>
    {
        public long Id { get; set; }
    }
}
