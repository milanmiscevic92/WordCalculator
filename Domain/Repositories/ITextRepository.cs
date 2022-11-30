using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITextRepository
    {
        Task<Text> GetTextByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
