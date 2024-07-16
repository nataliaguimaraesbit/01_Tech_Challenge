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

        public async Task<PagedResponse<List<Contact>?>> GetByFilter(GetByParamsRequest request)
        {

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
                .RuleFor(c => c.DDD, f => f.PickRandom(GetValidDDDs()))
                .RuleFor(c => c.Email, f => f.Internet.Email());

            return faker.Generate(numberOfContacts);
        }

        #region Methods Private

        private List<string> GetValidDDDs()
        {
            return new List<string>
            {
                "11", "12", "13", "14", "15", "16", "17", "18", "19", // SP
                "21", "22", "24", // RJ
                "27", "28", // ES
                "31", "32", "33", "34", "35", "37", "38", // MG
                "41", "42", "43", "44", "45", "46", // PR
                "47", "48", "49", // SC
                "51", "53", "54", "55", // RS
                "61", // DF
                "62", "64", // GO
                "63", // TO
                "65", "66", // MT
                "67", // MS
                "68", // AC
                "69", // RO
                "71", "73", "74", "75", "77", // BA
                "79", // SE
                "81", "82", "83", "84", "85", "86", "87", "88", "89", // NE
                "91", "92", "93", "94", "95", "96", "97", "98", "99" // N
            };
        }

        #endregion
    }
}
