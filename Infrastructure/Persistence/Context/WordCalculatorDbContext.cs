using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Context
{
    public class WordCalculatorDbContext : DbContext, IWordCalculatorDbContext
    {
        public WordCalculatorDbContext(DbContextOptions<WordCalculatorDbContext> options) : base(options) { }

        public DbSet<Text> Texts => Set<Text>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Text>().HasData(new Text(1, "One Two Three Four Five Six Seven"));

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
