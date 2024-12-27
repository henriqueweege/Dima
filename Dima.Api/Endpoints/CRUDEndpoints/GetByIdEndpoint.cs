using Dima.Api.Common.Api;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Core.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Dima.Core.Requests;
using System.Security.Claims;

namespace Dima.Api.Endpoints.CRUDEndpoints
{
    public class GetByIdEndpoint<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest> : IEndpoint where TModel : BaseModel 
    {
        private static string Url = string.Empty;
        public GetByIdEndpoint(string url)
        {
            Url = url;
        }

        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet($"{Url}/{{id}}", HandleAsync)
            .WithName($"{typeof(TModel).Name}: GetById")
            .WithSummary($"Get {typeof(TModel).Name}.")
            .WithDescription($"Get {typeof(TModel).Name}.")
            .Produces<Response<TModel?>>();

        private static async Task<IResult> HandleAsync([FromQuery] long id, [FromServices] ICRUDHandler<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest> handler, ClaimsPrincipal user)
        {
            var res = await handler.Handle(id, user.Identity!.Name!);

            return res.IsSuccess ? Results.Ok(res) : Results.BadRequest(res);
        }
    }
}
