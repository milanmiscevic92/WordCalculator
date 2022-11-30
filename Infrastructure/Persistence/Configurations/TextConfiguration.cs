using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TextConfiguration : IEntityTypeConfiguration<Text>
    {
        public void Configure(EntityTypeBuilder<Text> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired();
        }
    }
}
