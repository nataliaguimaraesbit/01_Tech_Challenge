using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Application.Services;
using LocalFriendzApi.Core.Configuration;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Infrastructure.Data;
using LocalFriendzApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IContactServices, ContactServices>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();
ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
                x =>
                {
                    x.UseSqlServer(ApiConfiguration.ConnectionString);
                });


var app = builder.Build();

app.MapPost("api/create-contact", async (IContactServices contactServices, CreateContactRequest request) =>
{

    var response = await contactServices.CreateAsync(request);

    return Results.Created($"/api/create-contact/", response);


}).WithOpenApi()
            .WithName("LocalFriendz")
            .WithTags("Posts")
            .WithSummary("Create a new Contact")
            .WithDescription("Endpoint to create a new Contact.")
            .Produces((int)HttpStatusCode.Created)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
