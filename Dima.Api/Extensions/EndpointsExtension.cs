using Dima.Api.Common.Api;
using Dima.Api.Endpoints.CategoryEndpoints;
using Dima.Api.Endpoints.Identity;
using Dima.Api.Endpoints.TransactionEndpoints;
using Dima.Api.Models;

namespace Dima.Api.Extensions
{
    public static class EndpointsExtension
    {
        public static void AddEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("v1/categories")
                .RequireAuthorization()
                .WithTags("Categories")
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetByIdCategoryEndpoint>()
                .MapEndpoint<GetAllCategoryEndpoint>();

            endpoints.MapGroup("v1/transactions")
                .RequireAuthorization()
                .WithTags("Transactions")
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetByIdTransactionEndpoint>()
                .MapEndpoint<GetByRangeDateTransactionEndpoint>();

            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapEndpoint<LogoutEndpoint>()
                .MapEndpoint<GetRolesEndpoint>();

        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
