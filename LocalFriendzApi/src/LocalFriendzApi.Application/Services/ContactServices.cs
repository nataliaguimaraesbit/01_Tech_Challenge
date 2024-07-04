using Bogus;
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

            return response;
        }
        public async Task<PagedResponse<List<Contact>?>> GetAll(GetAllContactRequest request)
        {
            var response = await _contactRepository.GetAll(request);
            return response;
        }

        public async Task<Response<Contact?>> PutContact(Guid id, UpdateContactRequest request)
        {
            var response = await _contactRepository.Update(id, request);
            return response;
        }

        public async Task<Response<Contact?>> DeleteContact(Guid id)
        {
            var response = await _contactRepository.Delete(id);
            return response;
        }

        public async Task<PagedResponse<List<Contact>?>> GetByFilter(string codeRegion)
        {
            var request = GetByCodeRegionRequest.RequestMapper(codeRegion);

            var response = await _contactRepository.GetContactByFilter(request);
            return response;
        }

        public IEnumerable<Contact> ContactGenerator(int numberOfContacts)
        {
            var faker = new Faker<Contact>()
                .RuleFor(c => c.IdContact, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.Phone, f => {
                    string phoneNumber = f.Phone.PhoneNumber();
                    return phoneNumber.Length <= 20 ? phoneNumber : phoneNumber.Substring(0, 20);
                })
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.AreaCode, f => new AreaCode { CodeRegion = f.Address.ZipCode().Substring(0, 2) });

            return faker.Generate(numberOfContacts);
        }
    }
}
