using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? string.Empty;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x=>x.CustomSchemaIds(n=> n.FullName));
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseMySql(cnnStr, ServerVersion.AutoDetect(cnnStr));
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", teste);

void teste(AppDbContext context)
{
    var cat = context.Transactions.FirstOrDefault(x => x.Id == 2);

    cat.PaidOrReceivedAt = DateOnly.MaxValue;

    context.Transactions.Update(cat);
    context.SaveChanges();

    var x = context.Transactions.FirstOrDefault(x => x.Id == cat.Id);

}

app.Run();
