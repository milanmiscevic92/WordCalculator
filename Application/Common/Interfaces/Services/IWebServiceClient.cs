using Application.Common.Responses;

namespace Application.Common.Interfaces.Services
{
    public interface IWebServiceClient
    {
        Task<ApiResponse<T>> SendDataAsync<T>(string url, object data);
    }
}
