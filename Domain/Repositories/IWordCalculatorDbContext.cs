using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IWordCalculatorDbContext
    {
        public DbSet<Text> Texts { get; }
    }
}
