namespace LocalFriendzApi.Core.Models
{
    public class AreaCode
    {
        public Guid IdAreaCode { get; set; }
        public string? CodeRegion { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
