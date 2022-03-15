using EF.Core.Training.BlackBox;
using Microsoft.EntityFrameworkCore;

namespace EF.Core.Training
{
    /// <summary>
    /// Entity Framework Core Repository.
    /// This is where you will implement the IRepositories methods used by the Unit Tests.
    /// </summary>
    public class EfRepository : IRepository
    {
        #region Do Not Alter
        private readonly ApiContext apiContext;
        public EfRepository()
        {
            apiContext = new ApiContext();
        }
        public async Task MigrateDb()
        {
            await apiContext.Database.MigrateAsync();
        }
        #endregion

        // ALL CODE CHANGES SHOULD HAPPEN BELOW THIS COMMENT

        public async Task<Author> CreateAuthor(Author author)
        {
            apiContext.Author.Add(author);
            await apiContext.SaveChangesAsync();
            return author;
        }

        public async Task<AuthorBookLink> CreateAuthorBookLink(AuthorBookLink link)
        {
            apiContext.AuthorBookLinks.Add(link);
            await apiContext.SaveChangesAsync();
            return link;
        }

        public async Task<Book> CreateBook(Book book)
        {
            apiContext.Books.Add(book);
            await apiContext.SaveChangesAsync();
            return book;
        }

        public async Task<BookGenreLink> CreateBookGenreLink(BookGenreLink link)
        {
            apiContext.BookGenreLinks.Add(link);
            await apiContext.SaveChangesAsync();
            return link;
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            apiContext.Genres.Add(genre);
            await apiContext.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> DeleteAuthor(Author author)
        {
            try
            {
                apiContext.Remove(author);
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (InvalidOperationException)
            {
                apiContext.Entry(author).State = EntityState.Unchanged;
                return false;
            }
        }

        public async Task<bool> DeleteAuthorBookLink(AuthorBookLink link)
        {
            apiContext.Remove(link);
            return await apiContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAuthorBookLinksForBook(int bookID)
        {
            var authorLink = await apiContext.AuthorBookLinks.FirstOrDefaultAsync(x => x.BookID == bookID);
            apiContext.Remove(authorLink);
            return await apiContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBook(Book book)
        {
            apiContext.Remove(book);
            return await apiContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBookGenreLink(BookGenreLink link)
        {
            apiContext.Remove(link);
            return await apiContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBookGenreLinksForBook(int bookID)
        {
            apiContext.RemoveRange(apiContext.BookGenreLinks.Where(x => x.BookID == bookID));
            return await apiContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGenre(Genre genre)
        {
            try
            {
                await genre.DoBeforeDelete(this);
                apiContext.Genres.Remove(genre);
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch
            {
                apiContext.Entry(genre).State = EntityState.Unchanged;
                return false;
            }
        }

        public async Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByAuthorID(int authorID)
        {
            return await apiContext.AuthorBookLinks.Where(x => x.AuthorID == authorID).ToListAsync();
        }

        public async Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByBookID(int bookID)
        {
            return await apiContext.AuthorBookLinks.Where(x => x.BookID == bookID).ToListAsync();
        }

        public async Task<Author> RetrieveAuthorByID(int authorID)
        {
            return await apiContext.Author.FirstOrDefaultAsync(x => x.ID == authorID);
        }

        public async Task<ICollection<Author>> RetrieveAuthors()
        {
            return await apiContext.Author.ToListAsync();
        }

        public async Task<ICollection<Author>> RetrieveAuthorsByBookID(int bookID)
        {
            return await apiContext.Author.Join(
                    apiContext.AuthorBookLinks,
                    a => a.ID,
                    l => l.AuthorID,
                    (author, link) => new { author, link.BookID })
                .Where(x => x.BookID == bookID)
                .Select(x => x.author).ToListAsync();
        }

        public async Task<Book> RetrieveBookByID(int bookID)
        {
            return await apiContext.Books.FirstOrDefaultAsync(x => x.ID == bookID);
        }

        public async Task<ICollection<BookGenreLink>> RetrieveBookGenreLinksByBookID(int bookID)
        {
            return await apiContext.BookGenreLinks.Where(x => x.BookID == bookID).ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooks()
        {
            return await apiContext.Books.ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooksByAuthorID(int authorID)
        {
            return await apiContext.Books.Join(
                    apiContext.AuthorBookLinks,
                    a => a.ID,
                    l => l.BookID,
                    (book, link) => new { book, link.AuthorID })
                .Where(x => x.AuthorID == authorID)
                .Select(x => x.book).ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooksByGenreID(int genreID)
        {
            return await apiContext.Books.Join(
                    apiContext.BookGenreLinks,
                    a => a.ID,
                    l => l.BookID,
                    (book, link) => new { book, link.GenreID })
                .Where(x => x.GenreID == genreID)
                .Select(x => x.book).ToListAsync();
        }

        public async Task<Genre> RetrieveGenreByID(int genreID)
        {
            return await apiContext.Genres.FirstOrDefaultAsync(x => x.ID == genreID);
        }

        public async Task<ICollection<Genre>> RetrieveGenres()
        {
            return await apiContext.Genres.ToListAsync();
        }

        public async Task<ICollection<Genre>> RetrieveGenresByBookID(int bookID)
        {
            return await apiContext.Genres.Include(x => x.BookLinks)
                .Where(x => x.BookLinks.Any(l => l.BookID == bookID)).ToListAsync();
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            apiContext.Author.Update(author);
            await apiContext.SaveChangesAsync();

            return author;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            apiContext.Books.Update(book);
            await apiContext.SaveChangesAsync();

            return book;
        }

        public async Task<Genre> UpdateGenre(Genre genre)
        {
            apiContext.Genres.Update(genre);
            await apiContext.SaveChangesAsync();

            return genre;
        }
    }
}
