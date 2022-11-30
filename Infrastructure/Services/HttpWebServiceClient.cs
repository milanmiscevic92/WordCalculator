using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Services
{
    internal sealed class HttpWebServiceClient : IWebServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpWebServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<ApiResponse<T>> SendDataAsync<T>(string url, object data)
        {
            var httpClient = _httpClientFactory.CreateClient();

            using var request = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri(url)
            };

            using var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                if (typeof(T) == typeof(string))
                {
                    var operationResult = new ApiResponse<string>() { Data = responseData, Success = true };
                    return operationResult as ApiResponse<T>;
                }
                else
                {
                    var deserializedData = JsonConvert.DeserializeObject<T>(responseData);
                    var apiResponse = new ApiResponse<T>() { Data = deserializedData, Success = true };
                    return apiResponse;
                }
            }
            else
            {
                var responseError = await response.Content.ReadAsStringAsync();
                var operationResult = new ApiResponse<string>() { Message = responseError, Success = false };
                return operationResult as ApiResponse<T>;
            }
        }
    }
}