namespace LocalFriendzApi.Core.Models
{
    public class Contact : BaseModel
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public AreaCode? AreaCode { get; set; }
    }
}
