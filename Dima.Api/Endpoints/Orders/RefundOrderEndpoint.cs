using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class RefundOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}/refund", HandleAsync).WithName("Orders: Refund").WithDescription("Estorna um pedido").WithOrder(6).Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IOrderHandler handler, long id, ClaimsPrincipal user)
        {
            
            var res = await handler.HandleAsync(new RefundOrderRequest { Id = id, UserId = user.Identity!.Name ?? string.Empty});

            return res.IsSuccess ? TypedResults.Ok(res) : TypedResults.BadRequest(res);
        }
    }
}
