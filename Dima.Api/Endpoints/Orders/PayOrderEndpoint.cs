using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class PayOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}/pay", HandleAsync).WithName("ORders: Pay").WithDescription("Paga um pedido").WithOrder(3).Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IOrderHandler handler, PayOrderRequest request, long id, ClaimsPrincipal user)
        {
            request.Id = id;
            request.UserId = user.Identity!.Name ?? string.Empty;

            var res = await handler.HandleAsync(request);

            return res.IsSuccess ? TypedResults.Ok(res) : TypedResults.BadRequest(res);
        }
    }
}
