using EF.Core.Training.BlackBox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Core.Training.CodeShop.Mappings
{
    public class AuthorBookLinkMapping : IEntityTypeConfiguration<AuthorBookLink>
    {
        public void Configure(EntityTypeBuilder<AuthorBookLink> builder)
        {
            builder.ToTable("AuthorBookLink");
            builder.HasKey(e => new { e.AuthorID, e.BookID });

            builder.HasOne(authorBookLink => authorBookLink.Book).WithMany(book => book.AuthorLinks)
                .HasForeignKey(authorBookLink => authorBookLink.BookID);

            builder.HasOne(authorBookLink => authorBookLink.Author).WithMany(author => author.BookLinks)
                .HasForeignKey(authorBookLink => authorBookLink.AuthorID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
