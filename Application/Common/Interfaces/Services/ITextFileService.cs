namespace Application.Common.Interfaces.Services
{
    public interface ITextFileService
    {
        Task<string> GetTextFromFileByPathAsync(string path, CancellationToken cancellationToken = default);
    }
}
