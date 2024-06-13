using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Infrastructure.Data;

namespace LocalFriendzApi.Infrastructure.Repositories
{
    public class ContactRepository(AppDbContext context) : IContactRepository
    {
        public async Task<Contact> Create(Contact contact)
        {
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();

            return contact;
        }
    }
}
