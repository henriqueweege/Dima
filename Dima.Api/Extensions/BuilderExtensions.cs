using Dima.Api.Data;
using Dima.Api.Handlers.CategoryHandler;
using Dima.Api.Handlers.TransactionHandler;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Extensions
{
    public static class BuilderExtensions
    {

        public static void AddConfigurations(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetSection("BackendUrl").Value ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetSection("FrontendUrl").Value ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {

            builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));
            builder.Services.AddEndpointsApiExplorer();
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
            builder.Services.AddAuthorization();
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseMySql(Configuration.ConnectionString, ServerVersion.AutoDetect(Configuration.ConnectionString));
            });

            builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole<long>>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
            builder.Services.AddTransient<ICRUDHandler<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory, GetByIdCategory>, CategoryHandler>();
            builder.Services.AddTransient<ICRUDHandler<Transaction, CreateTransaction, UpdateTransaction, DeleteTransaction, GetByDateRangeTransaction, GetByIdTransaction>, TransactionHandler>();
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(opt => opt.AddPolicy(ApiConfiguration.CorsPolicyName, policy => policy.WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl]).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
        }
    }
}
