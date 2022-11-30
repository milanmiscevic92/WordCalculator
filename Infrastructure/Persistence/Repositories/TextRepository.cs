using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal sealed class TextRepository : ITextRepository
    {
        private readonly IWordCalculatorDbContext _context;

        public TextRepository(WordCalculatorDbContext context)
        {
            _context = context;
        }

        public async Task<Text> GetTextByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Texts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
