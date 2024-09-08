using Dima.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurations();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors();
app.UseSecurity();

app.AddEndpoints();

app.Run();
