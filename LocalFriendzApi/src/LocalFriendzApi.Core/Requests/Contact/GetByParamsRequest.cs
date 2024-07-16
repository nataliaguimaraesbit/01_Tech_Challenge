namespace LocalFriendzApi.Core.Requests.Contact
{
    public class GetByParamsRequest : PagedRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? DDD { get; set; }
        public string? Email { get; set; }
    }
}
