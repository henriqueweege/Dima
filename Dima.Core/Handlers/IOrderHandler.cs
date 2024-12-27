using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Core.Handlers
{
    public interface IOrderHandler
    {
        Task<Response<Order?>> HandleAsync(CancelOrderRequest request);
        Task<Response<Order?>> HandleAsync(CreateOrderRequest request);
        Task<Response<Order?>> HandleAsync(PayOrderRequest request);
        Task<Response<Order?>> HandleAsync(RefundOrderRequest request);
        Task<PagedResponse<List<Order?>>> HandleAsync(GetAllOrdersRequest request);
        Task<Response<Order?>> HandleAsync(GetOrderByNumberRequest request);
    }
}
