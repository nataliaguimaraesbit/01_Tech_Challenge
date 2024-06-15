using Azure;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalFriendzApi.Infrastructure.Repositories
{
    public class ContactRepository(AppDbContext context) : IContactRepository
    {
        public async Task<Contact?> Create(Contact contact)
        {
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();

            return contact;
        }

        public async Task<List<Contact?>> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await context.Contacts.ToListAsync();
            }
            else
            {
                return await context.Contacts.Where(a => a.Name.Equals(name)).ToListAsync();
            }

        }

        public async Task<Contact?> Update(Guid id, UpdateContactRequest request)
        {
            var response = await context.Contacts
                                        .Include(a => a.AreaCode)
                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (response is null)
            {
                return null;
            }

            response.Name = request.Name;
            await context.SaveChangesAsync();

            return response;
        }

        public async Task<Contact?> Delete(Guid id)
        {
            var response = await context.Contacts
                                        .Include(a => a.AreaCode)
                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (response is null)
            {
                return null;
            }

            context.Contacts.Remove(response);
            await context.SaveChangesAsync();

            return response;
        }
    }
}
