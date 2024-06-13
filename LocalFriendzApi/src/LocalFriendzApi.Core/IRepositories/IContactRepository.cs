using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Core.IRepositories
{
    public interface IContactRepository
    {
        Task<Response<Contact?>> CreateAsync(CreateContactRequest request);
    }
}
