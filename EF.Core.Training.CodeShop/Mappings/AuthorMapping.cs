using EF.Core.Training.BlackBox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Core.Training.CodeShop.Mappings
{
    public class AuthorMapping : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");
            builder.HasKey("ID");
            builder.Property(e => e.ID).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasColumnType("TEXT").IsRequired();
            builder.Property(e => e.First).HasColumnType("TEXT");
            builder.Property(e => e.Last).HasColumnType("TEXT");
            builder.Property(e => e.Bio).HasColumnType("TEXT");
        }
    }
}
