using LocalFriendzApi.Core.Configuration;
using System.Text.Json.Serialization;

namespace LocalFriendzApi.Core.Responses
{
    public class Response<TData>
    {
        private int _code = ConfigurationPage.DefaultStatusCode;

        [JsonConstructor]
        public Response() => _code = ConfigurationPage.DefaultStatusCode;

        public Response(TData? data, int code = ConfigurationPage.DefaultStatusCode, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }

        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;
    }
}
