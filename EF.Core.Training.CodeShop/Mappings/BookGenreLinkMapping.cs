using EF.Core.Training.BlackBox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Core.Training.CodeShop.Mappings
{
    public class BookGenreLinkMapping : IEntityTypeConfiguration<BookGenreLink>
    {
        public void Configure(EntityTypeBuilder<BookGenreLink> builder)
        {
            builder.ToTable("BookGenreLink");
            builder.HasKey(e => new { e.BookID, e.GenreID });

            builder.HasOne(e => e.Book).WithMany(x => x.GenreLinks)
                .HasForeignKey(e => e.BookID);

            builder.HasOne(e => e.Genre).WithMany(x => x.BookLinks)
                .HasForeignKey(e => e.GenreID);
        }
    }
}
