using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;

namespace LocalFriendzApi.Core.IRepositories
{
    public interface IContactRepository
    {
        Task<Contact?> Create(Contact contact);
        Task<List<Contact?>> Search(string name);
        Task<Contact?> Update(Guid id, UpdateContactRequest request);
        Task<Contact?> Delete(Guid id);
    }
}
