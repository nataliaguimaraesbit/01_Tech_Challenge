namespace LocalFriendzApi.Core.Requests.Contact
{
    public class GetByCodeRegionRequest : PagedRequest
    {
        public string CodeRegion { get; set; }

        public static GetByCodeRegionRequest RequestMapper(string codeRegion)
        {
            return new GetByCodeRegionRequest() { CodeRegion = codeRegion };
        }
    }
}
