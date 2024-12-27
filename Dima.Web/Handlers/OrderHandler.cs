using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class OrderHandler(IHttpClientFactory factory) : IOrderHandler
    {
        private readonly HttpClient client = factory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Order?>> HandleAsync(CancelOrderRequest request)
        {
            var res = await client.PostAsJsonAsync($"v1/orders/{request.Id}/cancel", request);

            return await res.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Não foi possível cancelar o pedido");

        }

        public async Task<Response<Order?>> HandleAsync(CreateOrderRequest request)
        {
            var res = await client.PostAsJsonAsync($"v1/orders", request);

            return await res.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Não foi possível criar o pedido");
        }

        public async Task<Response<Order?>> HandleAsync(PayOrderRequest request)
        {
            var res = await client.PostAsJsonAsync($"v1/orders/{request.Id}/pay", request);

            return await res.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Não foi possível pagar o pedido");
        }

        public async Task<Response<Order?>> HandleAsync(RefundOrderRequest request)
        {
            var res = await client.PostAsJsonAsync($"v1/orders/{request.Id}/refund", request);

            return await res.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Não foi possível pagar o pedido");
        }

        public async Task<PagedResponse<List<Order?>>> HandleAsync(GetAllOrdersRequest request)
        => await client.GetFromJsonAsync<PagedResponse<List<Order?>>>("v1/orders") ?? new PagedResponse<List<Order?>>(null, 400, "Erro ao buscar pedidos");

        public async Task<Response<Order?>> HandleAsync(GetOrderByNumberRequest request)
        {
            return await client.GetFromJsonAsync<Response<Order?>>($"v1/orders/{request.Number}") ?? new Response<Order?>(null, 400, "Não foi possível obter o pedido");
        }
    }
}
