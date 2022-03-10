using EF.Core.Training.BlackBox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Core.Training.CodeShop.Mappings
{
    public class GenreMapping : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre");
            builder.HasKey(e => e.ID);

            builder.Property(e => e.Name).HasColumnType("TEXT").IsRequired();
        }
    }
}
