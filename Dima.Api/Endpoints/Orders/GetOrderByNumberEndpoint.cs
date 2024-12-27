using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class GetOrderByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{number}", HandleAsync).WithName("Orders: GetByNumber").WithDescription("Recupera pedido pelo numbero").WithOrder(6).Produces<PagedResponse<Order?>>();

        private static async Task<IResult> HandleAsync(IOrderHandler handler, ClaimsPrincipal user, string number)
        {

            var res = await handler.HandleAsync(new GetOrderByNumberRequest { UserId = user.Identity!.Name ?? string.Empty, Number = number });

            return res.IsSuccess ? TypedResults.Ok(res) : TypedResults.BadRequest(res);
        }
    }
}
