using Azure.Core;
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

app.MapGet("api/list-all-by-parameters/{name}", async (IContactServices contactServices, string name = " ") =>
{
    var response = await contactServices.GetAsync(name);

    return Results.Created($"/api/list-all-by-parameters/{name}", response);

}).WithOpenApi()
              //.WithName("LocalFriendz")
              .WithTags("Gets")
              .WithSummary("Get Contacts")
              .WithDescription("Endpoint to get contacts.")
              .Produces((int)HttpStatusCode.Created)
              .Produces((int)HttpStatusCode.NotFound)
              .Produces((int)HttpStatusCode.InternalServerError);


app.MapPut("update/{id}", async (IContactServices contactServices, Guid id, UpdateContactRequest request) =>
{
    var response = await contactServices.PutContact(id,request);

    if(response is null)
    {
        Results.NotFound();
    }
    Results.NoContent();


}).WithOpenApi()
              .WithTags("Puts")
              .WithSummary("Put Contacts")
              .WithDescription("Endpoint to put contacts.")
              .Produces((int)HttpStatusCode.Created)
              .Produces((int)HttpStatusCode.NotFound)
              .Produces((int)HttpStatusCode.InternalServerError);

app.MapDelete("remove/{id}", async (IContactServices contactServices, Guid id) =>
{
    var response = await contactServices.DeleteContact(id);
    return Results.Ok(response);

}).WithOpenApi()
            .WithName("DeleteTodo")
            .WithTags("Deletes")
            .WithSummary("Delete a task")
            .WithDescription("Endpoint to delete an existing task based on the provided ID.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
