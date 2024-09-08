using Dima.Api.Common.Api;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Core.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.CRUDEndpoints
{
    public class UpdateEndpoint<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest,
     TGetByIdRequest> : IEndpoint where TModel : BaseModel
    {
        private static string Url = string.Empty;
        public UpdateEndpoint(string url)
        {
            Url = url;
        }

        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut($"{Url}", HandleAsync)
            .WithName($"{typeof(TModel).Name}: Update")
            .WithSummary($"Atualiza {typeof(TModel).Name}.")
            .WithDescription($"Atualiza {typeof(TModel).Name}.")
            .Produces<Response<TModel?>>();


        private static async Task<IResult> HandleAsync([FromBody]TUpdateRequest request, [FromServices] ICRUDHandler<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest, TGetByIdRequest> handler)
        {
            var res = await handler.Handle(request);

            return res.IsSuccess ? Results.Ok(res) : Results.BadRequest(res);
        }
    }
}
