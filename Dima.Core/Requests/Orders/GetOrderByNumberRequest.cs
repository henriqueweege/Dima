using Dima.Core.Models;


namespace Dima.Core.Requests.Orders
{
    public class GetOrderByNumberRequest : BaseRequest<Order>
    {
        public string Number { get; set; }
    }
}
