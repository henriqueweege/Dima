using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class CancelOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}/cancel", HandleAsync).WithName("Orders: CancelOrder").WithDescription("Cancela Pedido").WithOrder(2).Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IOrderHandler handler, long id, ClaimsPrincipal user)
        {
            var res = await handler.HandleAsync(new Core.Requests.Orders.CancelOrderRequest()
            {
                Id = id,
                UserId = user.Identity.Name ?? string.Empty
            });

            return res.IsSuccess ? TypedResults.Ok(res) : TypedResults.BadRequest(res);
        }
    }
}
