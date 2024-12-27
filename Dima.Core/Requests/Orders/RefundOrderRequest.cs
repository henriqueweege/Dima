
using Dima.Core.Models;

namespace Dima.Core.Requests.Orders
{
    public class RefundOrderRequest : BaseRequest<Order>
    {
        public long Id { get; set; }
    }
}
