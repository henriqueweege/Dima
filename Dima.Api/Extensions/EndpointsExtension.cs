using Dima.Api.Common.Api;
using Dima.Api.Endpoints.CategoryEndpoints;
using Dima.Api.Endpoints.Identity;
using Dima.Api.Endpoints.Orders;
using Dima.Api.Endpoints.Reports;
using Dima.Api.Endpoints.Stripe;
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

            endpoints.MapGroup("v1/reports")
                .WithTags("Reports")
                .RequireAuthorization()
                .MapEndpoint<GetExpensesByCategoryEndpoint>()
                .MapEndpoint<GetFinancialSummaryEndpoint>()
                .MapEndpoint<GetIncomesAndExpensesEndpoint>()
                .MapEndpoint<GetIncomesByCategoriesEndpoint>();

            endpoints.MapGroup("v1/products")
                .RequireAuthorization()
                .WithTags("Products")
                .MapEndpoint<GetAllProductsEndpoint>()
                .MapEndpoint<GetProductBySlugEndpoint>();

            endpoints.MapGroup("v1/voucher")
                .RequireAuthorization()
                .WithTags("Vouchers")
                .MapEndpoint<GetVoucherByNumberEndpoint>();

            endpoints.MapGroup("v1/order")
                .RequireAuthorization()
                .WithTags("Orders")
                .MapEndpoint<GetAllOrdersEndpoint>()
                .MapEndpoint<GetOrderByNumberEndpoint>()
                .MapEndpoint<CreateOrderEndpoint>()
                .MapEndpoint<CancelOrderEndpoint>()
                .MapEndpoint<PayOrderEndpoint>()
                .MapEndpoint<RefundOrderEndpoint>();

            endpoints.MapGroup("v1/payments/stripe")
                .WithTags("Payments stripe")
                .RequireAuthorization()
                .MapEndpoint<CreateSessionEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
