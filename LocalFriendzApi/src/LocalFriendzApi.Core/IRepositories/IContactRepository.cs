using LocalFriendzApi.Core.Models;

namespace LocalFriendzApi.Core.IRepositories
{
    public interface IContactRepository
    {
        Task<Contact> Create(Contact contact);
    }
}
