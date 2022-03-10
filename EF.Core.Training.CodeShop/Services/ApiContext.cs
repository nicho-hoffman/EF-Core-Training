using Microsoft.EntityFrameworkCore;
using EF.Core.Training.BlackBox;
using EF.Core.Training.CodeShop.Mappings;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenreMapping());
            modelBuilder.ApplyConfiguration(new BookGenreLinkMapping());
            modelBuilder.ApplyConfiguration(new BookMapping());
            modelBuilder.ApplyConfiguration(new AuthorMapping());
            modelBuilder.ApplyConfiguration(new AuthorBookLinkMapping());
        }
    }
}
