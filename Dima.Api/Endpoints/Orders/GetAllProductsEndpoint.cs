using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Orders
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync).WithName("Products: GetAll").WithDescription("Recupera todos os produtos").WithOrder(1).Produces<PagedResponse<List<Product?>>>();

        private static async Task<IResult> HandleAsync(IProductHandler handler, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {

            var res = await handler.HandleAsync(new GetAllProductsRequest { PageNumber = pageNumber, PageSize = pageSize });

            return res.IsSuccess ? TypedResults.Ok(res) : TypedResults.BadRequest(res);
        }
    }
}
