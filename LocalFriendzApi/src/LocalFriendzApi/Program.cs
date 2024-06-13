using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Application.Services;
using LocalFriendzApi.Core.Configuration;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Infrastructure.Data;
using LocalFriendzApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Text.Json;

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

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });


var app = builder.Build();

app.MapPost("api/create-contact", async (IContactServices contactServices, CreateContactRequest request) =>
{

    var response = await contactServices.CreateAsync(request);

    // ~~> NÃO ESTÁ FUNCIONANDO NA HORA DE RETORNAR NA TELA.
    return Results.Created($"/api/create-contact/{response.Data.Name}", response);


}).WithOpenApi()
            .WithName("CreateTodo")
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
