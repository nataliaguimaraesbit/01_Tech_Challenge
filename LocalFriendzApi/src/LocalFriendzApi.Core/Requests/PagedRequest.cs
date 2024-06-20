using LocalFriendzApi.Core.Configuration;

namespace LocalFriendzApi.Core.Requests
{
    public abstract class PagedRequest
    {
        public int PageSize { get; set; } = ConfigurationPage.DefaultPageSize;
        public int PageNumber { get; set; } = ConfigurationPage.DefaultPageNumber;
    }
}
