using Microsoft.EntityFrameworkCore;
using EF.Core.Training.BlackBox;

namespace EF.Core.Training
{
    public class ApiContext : DbContext
    {
        #region Do Not Alter
        private readonly string DbPath = "";
        public ApiContext()
        {
            // saves the SQLite EF.Core.Training.db to the solution's root folder
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\"));
            DbPath = Path.Join(path, "EF.Core.Training.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
        #endregion

        // ALL CODE CHANGES SHOULD HAPPEN BELOW THIS COMMENT

        
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenreLink> BookGenreLinks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBookLink> AuthorBookLinks { get; set; }

        // TODO : Add more DbSets<T> for the other two Models here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this entity is good to go, CRUD Unit Tests passing
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Name).HasColumnType("TEXT").IsRequired();
            });

            // this entity is good to go once Book is corrected
            modelBuilder.Entity<BookGenreLink>(entity =>
            {
                entity.ToTable("BookGenreLink");
                entity.HasKey(e => new { e.BookID, e.GenreID });

                entity.HasOne(e => e.Book).WithMany(x => x.GenreLinks)
                    .HasForeignKey(e => e.BookID);

                entity.HasOne(e => e.Genre).WithMany(x => x.BookLinks)
                    .HasForeignKey(e => e.GenreID);
            });

            // See https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/types for C# to SQLite DataTypes

            // this entity has some issues that prevent unit tests from passing
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");
                entity.HasKey("ID");

                // something might be missing on these ..
                entity.Property(e => e.ISBN).HasColumnType("TEXT").IsRequired(); 
                entity.Property(e => e.Title).HasColumnType("TEXT").IsRequired();
                entity.Property(e => e.Description).HasColumnType("TEXT");
                entity.Property(e => e.Price).HasColumnType("TEXT").IsRequired();
                entity.Property(e => e.Pages).HasColumnType("INTEGER").IsRequired();

                // something is wrong about these ..
                entity.HasMany(e => e.GenreLinks).WithOne(l => l.Book)
                    .HasForeignKey(l => l.BookID).OnDelete(DeleteBehavior.Cascade);
                entity.Ignore(e => e.AuthorLinks);
            });


            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).HasColumnType("TEXT").IsRequired();
                entity.Property(e => e.First).HasColumnType("TEXT");
                entity.Property(e => e.Last).HasColumnType("TEXT");
                entity.Property(e => e.Bio).HasColumnType("TEXT");
            });

            modelBuilder.Entity<AuthorBookLink>(entity =>
            {
                entity.ToTable("AuthorBookLink");
                entity.HasKey(e => new { e.BookID, e.AuthorID });

                entity.HasOne(e => e.Book).WithMany(x => x.AuthorLinks)
                    .HasForeignKey(e => e.BookID);

                entity.HasOne(e => e.Author).WithMany(x => x.BookLinks)
                    .HasForeignKey(e => e.AuthorID);
            });
        }
    }
}
