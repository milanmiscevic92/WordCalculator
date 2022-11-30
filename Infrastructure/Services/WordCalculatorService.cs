using Application.Common.Interfaces.Services;

namespace Infrastructure.Services
{
    internal sealed class WordCalculatorService : IWordCalculatorService
    {
        private readonly IWebServiceClient _webServiceClient;

        public WordCalculatorService(IWebServiceClient webServiceClient)
        {
            _webServiceClient = webServiceClient;
        }

        public async Task<int?> CalculateNumberOfWords(string text)
        {
            // this can also be moved to configuration
            var url = $"https://localhost:7128/api/WordCalculator/CalculateWords";

            var apiResponse = await _webServiceClient.SendDataAsync<int>(url, text);

            if (!apiResponse.Success)
            {
                return null;
            }

            return apiResponse.Data;
        }
    }
}
