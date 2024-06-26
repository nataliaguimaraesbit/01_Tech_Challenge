using LocalFriendzApi.Core.Requests.Contact;

namespace LocalFriendzApi.Core.Models
{
    public class AreaCode
    {
        public Guid IdAreaCode { get; set; }
        public string? CodeRegion { get; set; }

        public static AreaCode RequestMapper(CreateContactRequest request)
        {
            return new AreaCode()
            {
                IdAreaCode = Guid.NewGuid(),
                CodeRegion = request.CodeRegion
            };
        }
    }
}
