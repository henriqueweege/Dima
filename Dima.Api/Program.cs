using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers.CategoryHandler;
using Dima.Api.Handlers.TransactionHandler;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(x=>x.CustomSchemaIds(n=> n.FullName));

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? string.Empty;
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseMySql(cnnStr, ServerVersion.AutoDetect(cnnStr));
});

builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole<long>>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();

builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddTransient<ICRUDHandler<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory, GetByIdCategory>, CategoryHandler>();
builder.Services.AddTransient<ICRUDHandler<Transaction, CreateTransaction, UpdateTransaction, DeleteTransaction, GetByDateRangeTransaction, GetByIdTransaction>, TransactionHandler>();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.AddEndpoints();

app.Run();
