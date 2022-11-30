using Application.Common.Interfaces.Services;

namespace Infrastructure.Services
{
    internal sealed class TextFileService : ITextFileService
    {
        public async Task<string> GetTextFromFileByPathAsync(string path, CancellationToken cancellationToken = default)
        {
            using (var sr = new StreamReader(path))
            {
                var text = await sr.ReadToEndAsync().WaitAsync(cancellationToken).ConfigureAwait(false);

                return text;
            }
        }
    }
}
