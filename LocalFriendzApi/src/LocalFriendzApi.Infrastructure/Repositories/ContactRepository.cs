using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;
using LocalFriendzApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalFriendzApi.Infrastructure.Repositories
{
    public class ContactRepository(AppDbContext context) : IContactRepository
    {
        public async Task<Response<Contact?>> Create(Contact contact)
        {
            try
            {
                await context.Contacts.AddAsync(contact);
                await context.SaveChangesAsync();

                return new Response<Contact?>(contact, 201, "Contact created with sucess!");
            }
            catch (Exception)
            {
                return new Response<Contact?>(null, 500, "Internal server erro.");
            }
        }

        public async Task<Response<Contact?>> Update(Guid id, UpdateContactRequest request)
        {
            try
            {
                var contact = await context.Contacts
                                        .Include(a => a.AreaCode)
                                        .FirstOrDefaultAsync(a => a.Id == id);

                if (contact is null)
                {
                    return new Response<Contact?>(null, 404, "Contact not found!");
                }

                // Novos valores
                contact.Name = request.Name;
                contact.Email = request.Email;
                contact.Phone = request.Phone;
                contact.AreaCode.CodeRegion = request.AreaCode.CodeRegion;

                context.Contacts.Update(contact);
                await context.SaveChangesAsync();

                return new Response<Contact?>(contact, message: "Contact update with sucess!");
            }
            catch (Exception)
            {
                return new Response<Contact?>(null, 500, "Internal Server Erro!");
            }
        }

        public async Task<Response<Contact?>> Delete(Guid id)
        {
            try
            {
                var contact = await context.Contacts
                                        .Include(a => a.AreaCode)
                                        .FirstOrDefaultAsync(a => a.Id == id);

                if (contact is null)
                {
                    return new Response<Contact?>(null, 404, "Contact not found!");
                }

                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();

                return new Response<Contact?>(contact, message: "Removed contact with sucess!");
            }
            catch (Exception)
            {
                return new Response<Contact?>(null, 500, "Internal server erro!");
            }
        }

        public async Task<PagedResponse<List<Contact>?>> GetAll(GetAllContactRequest request)
        {
            try
            {
                var query = await context
                                  .Contacts
                                  .AsNoTracking()
                                  .Include(a => a.AreaCode)
                                  .OrderBy(c => c.Name)
                                  .ToListAsync();

                var contacts = query
                                   .Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToList();

                var count = query.Count();

                return new PagedResponse<List<Contact>?>(
                    contacts,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Contact>?>(null, 500, "Internal Server Erro!");
            }
        }

        public async Task<Response<Contact?>> GetContactByFilter(string codeRegion)
        {
            try
            {
                var contact = await context
                    .Contacts
                    .AsNoTracking()
                    .Include(a => a.AreaCode)
                    .FirstOrDefaultAsync(x => x.AreaCode.CodeRegion.Equals(codeRegion));

                return contact is null
                    ? new Response<Contact?>(null, 404, "Not found contact.")
                    : new Response<Contact?>(contact);
            }
            catch
            {
                return new Response<Contact?>(null, 500, "Internal server erro");
            }
        }
    }
}
