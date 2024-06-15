using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Application.IServices
{
    public interface IContactServices
    {
        Task<Response<Contact?>> CreateAsync(CreateContactRequest request);
        Task<Response<Contact?>> GetAsync(string name);
        Task<Contact> PutContact(Guid id, UpdateContactRequest request);
        Task<Contact> DeleteContact(Guid id);
    }
}
