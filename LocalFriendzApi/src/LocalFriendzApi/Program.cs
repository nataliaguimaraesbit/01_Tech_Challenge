using LocalFriendzApi.Commom.Api;
using LocalFriendzApi.Endpoints;
using LocalFriendzApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddServices();
builder.AddDocumentation();
builder.AddLogging();

var app = builder.Build();
app.MapEndpoints();


if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseLoggingMiddleware();
app.UseHttpsRedirection();

app.Run();
