using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Commom.Api;
using LocalFriendzApi.Core.Requests.Contact;
using System.Net;

namespace LocalFriendzApi.Endpoints
{
    public static class ContactEndpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var contactGroup = app.MapGroup("/Contact");

            contactGroup.MapPost("api/create-contact", async (IContactServices contactServices, CreateContactRequest request) =>
            {
                var response = await contactServices.CreateAsync(request);

                return response.ConfigureResponseStatus();

            })
            .WithTags("Contact")
            .WithName("Contact: Create Contact")
            .WithSummary("Create a new contact record.")
            .WithDescription("Creates and saves a new contact in the system. This endpoint requires valid contact details including name, email, and phone number. Returns the created contact information upon successful save.")
            .Produces((int)HttpStatusCode.Created)
            .Produces((int)HttpStatusCode.InternalServerError)
            .WithOpenApi();

            contactGroup.MapGet("api/list-all", async (IContactServices contactServices) =>
            {
                GetAllContactRequest request = new();
                var response = await contactServices.GetAll(request);

                return response.ConfigureResponseStatus();

            })
            .WithTags("Contact")
            .WithName("Contact: Gets Record")
            .WithSummary("Retrieve all contact records.")
            .WithDescription("Fetches and returns a list of all contact records stored in the system. This endpoint does not require any parameters and provides a comprehensive list of all available contacts.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError)
            .WithOpenApi();

            contactGroup.MapGet("api/list-by-filter", async (IContactServices contactServices, string codeRegion) =>
            {
                var response = await contactServices.GetByFilter(codeRegion);

                return response.ConfigureResponseStatus();

            })
            .WithTags("Contact")
            .WithName("Contact: Get Record")
            .WithSummary("Retrieve a contact record by filter.")
            .WithDescription("Fetches a contact record based on the specified filter criteria, such as coderegion. This endpoint requires a valid coderegion parameter to return the corresponding contact details.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError)
            .WithOpenApi();

            contactGroup.MapPut("api/update-contact", async (IContactServices contactServices, Guid id, UpdateContactRequest request) =>
            {
                var response = await contactServices.PutContact(id, request);

                return response.ConfigureResponseStatus();

            })
            .WithTags("Contact")
            .WithName("Contact: Update")
            .WithSummary("Update an existing contact record.")
            .WithDescription("Updates the details of an existing contact in the system. This endpoint requires the contact's unique identifier and the new information to be updated. If the contact exists, it will be updated with the provided details.")
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.InternalServerError)
            .WithOpenApi();

            contactGroup.MapDelete("api/delete-contact", async (IContactServices contactServices, Guid id) =>
            {
                var response = await contactServices.DeleteContact(id);

                return response.ConfigureResponseStatus();

            })
              .WithTags("Contact")
              .WithName("Contact: remove")
              .WithSummary("Remove an existing contact record.")
              .WithDescription("Deletes a specific contact from the system based on the provided identifier. This endpoint requires the unique identifier of the contact to be deleted. If the contact exists, it will be removed from the system.")
              .Produces((int)HttpStatusCode.OK)
              .Produces((int)HttpStatusCode.NotFound)
              .Produces((int)HttpStatusCode.InternalServerError)
              .WithOpenApi();
        }
    }
}
