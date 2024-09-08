using Dima.Api.Common.Api;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Core.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.CRUDEndpoints
{
    public class DeleteEndpoint<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest,
     TGetByIdRequest> : IEndpoint where TModel : BaseModel
    {
        private static string Url = string.Empty;
        public DeleteEndpoint(string url)
        {
            Url = url;
        }

        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete($"{Url}" , HandleAsync)
            .WithName($"{typeof(TModel).Name}: Delete")
            .WithSummary($"Delete {typeof(TModel).Name}.")
            .WithDescription($"Delete {typeof(TModel).Name}.")
            .Produces<Response<TModel?>>();

        private static async Task<IResult> HandleAsync([FromBody]TDeleteRequest request, [FromServices] ICRUDHandler<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest, TGetByIdRequest> handler)
        {
            var res = await handler.Handle(request);

            return res.IsSuccess ? Results.NoContent() : Results.BadRequest(res);
        }
    }
}
