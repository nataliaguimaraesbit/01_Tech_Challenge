using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LocalFriendzApi.Commom.Api
{
    public static class ConfigureResponseExtension
    {
        public static IResult ConfigureResponseStatus(this PagedResponse<List<Contact>?> response)
        {
            switch (response.Code)
            {
                case 200:
                    return TypedResults.Ok(response);
                case 201:
                    return TypedResults.Created(response.Data.FirstOrDefault().ToString());
                    break;
                case 400:
                    return TypedResults.BadRequest(response);
                    break;
                case 404:
                    return TypedResults.NotFound(response);
                    break;
                case 500:
                    var objectResult = new ObjectResult(response)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    return (IResult)objectResult;
                    break;
                default:
                    return TypedResults.NoContent();
            }
        }

        public static IResult ConfigureResponseStatus(this Response<Contact>? response)
        {
            switch (response.Code)
            {
                case 200:
                    return TypedResults.Ok(response);
                case 201:
                    return TypedResults.Created(response.Data.ToString());
                    break;
                case 400:
                    return TypedResults.BadRequest(response);
                    break;
                case 404:
                    return TypedResults.NotFound(response);
                    break;
                case 500:
                    var objectResult = new ObjectResult(response)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    return (IResult)objectResult;
                    break;
                default:
                    return TypedResults.NoContent();
            }
        }
    }
}
