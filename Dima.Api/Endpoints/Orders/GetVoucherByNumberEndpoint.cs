using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Orders
{
    public class GetVoucherByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{number}", HandleAsync).WithName("Voucher: GetByNumber").WithDescription("Recupera voucher por numbero").WithOrder(1).Produces<Response<Voucher?>>();

        private static async Task<IResult> HandleAsync(IVoucherHandler handler, string number)
        {
            var res = await handler.HandleAsync(new GetVoucherByNumberRequest { Number = number });

            return res.IsSuccess ? TypedResults.Ok(res) : TypedResults.BadRequest(res);
        }
    }
}
