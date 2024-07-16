using FluentValidation;
using LocalFriendzApi.Commom.Api;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Validations;
using LocalFriendzApi.Endpoints;
using LocalFriendzApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateContactRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateContactRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetByParamsRequestValidator>();

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
