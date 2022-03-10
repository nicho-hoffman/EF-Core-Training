using EF.Core.Training.BlackBox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Core.Training.CodeShop.Mappings
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey("ID");

            builder.Property(e => e.ISBN).HasColumnType("TEXT").IsRequired();
            builder.Property(e => e.Title).HasColumnType("TEXT").IsRequired();
            builder.Property(e => e.Description).HasColumnType("TEXT");
            builder.Property(e => e.Price).HasColumnType("TEXT").IsRequired();
            builder.Property(e => e.Pages).HasColumnType("INTEGER").IsRequired();
        }
    }
}
