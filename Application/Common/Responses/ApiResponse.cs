using Application.Common.Extensions;
using Newtonsoft.Json;
using System.Net;

namespace Application.Common.Responses
{
    public class ApiResponse
    {
        public object Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public object Errors { get; set; }

        public ApiResponse()
        {
        }

        public static ApiResponse Create(int statusCode, object result)
        {
            var isErrorStatusCode = statusCode >= (int)HttpStatusCode.BadRequest;
            ErrorResponse error = null;

            if (isErrorStatusCode && result.Exist())
            {
                error = JsonConvert.DeserializeObject<ErrorResponse>(result.ToString());
            }

            return new ApiResponse
            {
                Data = isErrorStatusCode ? null : result,
                Success = isErrorStatusCode ? false : true,
                Message = isErrorStatusCode ? !(error?.Message).Exist() ? result.Exist() ? result.ToString() : null : error?.Message : null,
                Errors = isErrorStatusCode ? error?.Errors : null
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public new T Data { get; set; }
    }
}
