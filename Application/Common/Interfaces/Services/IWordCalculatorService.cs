namespace Application.Common.Interfaces.Services
{
    public interface IWordCalculatorService
    {
        Task<int?> CalculateNumberOfWords(string text);
    }
}
