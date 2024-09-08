using Dima.Api.Common.Api;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Core.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Dima.Core.Requests;
using System.Security.Claims;

namespace Dima.Api.Endpoints.CRUDEndpoints
{
    public class GetByIdEndpoint<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest,
     TGetByIdRequest> : IEndpoint where TModel : BaseModel 
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

        private static async Task<IResult> HandleAsync([FromQuery] long id, [FromServices] ICRUDHandler<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest, TGetByIdRequest> handler, ClaimsPrincipal user)
        {
            var request = new BaseGetById<TModel>() {Id = id };
            request.UserId = user.Identity!.Name!;
            var res = await handler.Handle(request);

            return res.IsSuccess ? Results.Ok(res) : Results.BadRequest(res);
        }
    }
}
