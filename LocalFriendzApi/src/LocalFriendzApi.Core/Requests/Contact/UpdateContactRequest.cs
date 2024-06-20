using LocalFriendzApi.Core.Models;

namespace LocalFriendzApi.Core.Requests.Contact
{
    public class UpdateContactRequest
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public AreaCode? AreaCode { get; set; }
    }
}
