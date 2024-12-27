using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stripe;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Stripe
{
    public class CreateSessionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/session", HandleAsync).Produces<string?>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IStripeHandler handler, CreateSessionRequest request)
        {
            request.UserId = user.Identity!.Name ?? string.Empty;

            var res = await handler.HandleAsync(request);

            return res.IsSuccess ? TypedResults.Ok(res) : TypedResults.BadRequest(res);
        }
    }
}
