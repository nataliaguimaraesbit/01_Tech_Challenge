using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Application.Services
{
    public class ContactServices : IContactServices
    {
        private readonly IContactRepository? _contactRepository;

        public ContactServices(IContactRepository? contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Response<Contact?>> CreateAsync(CreateContactRequest request)
        {
            var contact = Contact.RequestMapper(request);
            var response = await _contactRepository.Create(contact);

            return new Response<Contact?>(response, 201, "Contato criada com sucesso!");
        }
        public async Task<Response<Contact>> GetAsync(string name)
        {
            var response = await _contactRepository.Search(name);

            return null;
        }

        public async Task<Contact> PutContact(Guid id, UpdateContactRequest request)
        {
            var response = await _contactRepository.Update(id, request);
            return response;
        }

        public async Task<Contact> DeleteContact(Guid id)
        {
            var response = await _contactRepository.Delete(id);
            return response;
        }
    }
}
