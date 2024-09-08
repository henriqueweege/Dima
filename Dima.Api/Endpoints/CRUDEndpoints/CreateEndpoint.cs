using Dima.Api.Common.Api;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Core.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.CRUDEndpoints;

public abstract class CreateEndpoint<TModel,  TCreateRequest,  TUpdateRequest,  TDeleteRequest,  TGetAllRequest,
     TGetByIdRequest> : IEndpoint where TModel : BaseModel
{
    private static string Url = string.Empty;
    public CreateEndpoint(string url)
    {
        Url = url;
    }

    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost($"{Url}", HandleAsync)
            .WithName($"{typeof(TModel).Name}: Create")
            .WithSummary($"Create {typeof(TModel).Name}.")
            .WithDescription($"Create {typeof(TModel).Name}.")
            .Produces<Response<TModel?>>();

    private static async Task<IResult> HandleAsync(TCreateRequest request, [FromServices] ICRUDHandler<TModel, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetAllRequest, TGetByIdRequest> handler)
    {
        var res = await handler.Handle(request);

        return res.IsSuccess ? Results.Created($"/{res.Data.Id}", res) : Results.BadRequest(res);
    }
}
