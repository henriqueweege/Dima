using Dima.Api.Common.Api;
using Dima.Api.Handlers.CategoryHandler;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Core.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dima.Core.Requests;

namespace Dima.Api.Endpoints.CRUDEndpoints
{
    public class GetAllEndpoint<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest,
     TGetByIdRequest> : IEndpoint where TModel : BaseModel where TGetAllRequest : BaseRequest<TModel>
    {
        private static string Url = string.Empty;
        public GetAllEndpoint(string url)
        {
            Url = url;
        }

        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet($"{Url}", HandleAsync)
            .WithName($"{typeof(TModel).Name}: GetAll")
            .WithSummary($"Get {typeof(TModel).Name}.")
            .WithDescription($"Get {typeof(TModel).Name}.")
            .Produces<Response<TModel?>>();

        private static async Task<IResult> HandleAsync([FromBody]TGetAllRequest request, [FromServices] ICRUDHandler<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest, TGetByIdRequest> handler, ClaimsPrincipal user)
        {
            request.UserId = user.Identity!.Name!;
            var res = await handler.Handle(request);

            return res.IsSuccess ? Results.Ok(res) : Results.BadRequest(res);
        }
    }
}
