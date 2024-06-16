using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;
using System.Net;

namespace LocalFriendzApi.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapPost("api/create-contact", async (IContactServices contactServices, CreateContactRequest request) =>
            {

                var response = await contactServices.CreateAsync(request);
                return response;

            }).WithOpenApi()
            .WithTags("Posts")
            .WithName("Contact: Create")
            .WithSummary("Create new contact.")
            .WithDescription("Save a new contact.")
            .Produces((int)HttpStatusCode.Created)
            .Produces((int)HttpStatusCode.InternalServerError)
            .Produces<Response<Contact?>>();

            app.MapGet("api/list-all", async (IContactServices contactServices) =>
            {
                GetAllContactRequest request = new();
                var response = await contactServices.GetAll(request);
                return response;

            }).WithOpenApi()
            .WithTags("Gets")
            .WithName("Contact: Gets Record")
            .WithSummary("Get all records.")
            .WithDescription("Get all contact.")
            .Produces((int)HttpStatusCode.Created)
            .Produces((int)HttpStatusCode.InternalServerError)
            .Produces<PagedResponse<List<Contact>?>>();

            app.MapPost("api/list-by-filter", async (IContactServices contactServices, GetAllByFilter request) =>
            {
                var response = await contactServices.GetByFilter(request);
                return response;

            }).WithOpenApi()
            .WithTags("Posts")
            .WithName("Contact: Gets by filter.")
            .WithSummary("Get by filter")
            .WithDescription("Get contacts use by filter.")
            .Produces((int)HttpStatusCode.Created)
            .Produces((int)HttpStatusCode.InternalServerError)
            .Produces<Response<Contact?>>();

            app.MapPut("api/update", async (IContactServices contactServices, Guid id, UpdateContactRequest request) =>
            {
                var response = await contactServices.PutContact(id, request);
                return response;


            }).WithOpenApi()
            .WithTags("Puts")
            .WithName("Contact: Update")
            .WithSummary("Update a contact.")
            .WithDescription("Update a contact if there is.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.InternalServerError)
            .Produces<Response<Contact?>>();

            app.MapDelete("api/remove", async (IContactServices contactServices, Guid id) =>
            {
                var response = await contactServices.DeleteContact(id);
                return response;

            }).WithOpenApi()
                        .WithTags("Delete")
                        .WithName("Contact: remove")
                        .WithSummary("Remove a contact.")
                        .WithDescription("Delete a specific contact if there is.")
                        .Produces((int)HttpStatusCode.OK)
                        .Produces((int)HttpStatusCode.InternalServerError)
                        .Produces<Response<Contact?>>();
        }
    }
}
