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

        // All the Console.WriteLine here is to represent log feature that could be implemented
        public async Task<Author> CreateAuthor(Author author)
        {
            try
            {
                apiContext.Authors.Add(author);
                await apiContext.SaveChangesAsync();
                return author;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Book> CreateBook(Book book)
        {
            try
            {
                apiContext.Books.Add(book);
                await apiContext.SaveChangesAsync();
                return book;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            try
            {
                apiContext.Genres.Add(genre);
                await apiContext.SaveChangesAsync();
                return genre;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<AuthorBookLink> CreateAuthorBookLink(AuthorBookLink link)
        {
            try
            {
                apiContext.AuthorBookLinks.Add(link);
                await apiContext.SaveChangesAsync();
                return link;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<BookGenreLink> CreateBookGenreLink(BookGenreLink link)
        {
            try
            {
                apiContext.BookGenreLinks.Add(link);
                await apiContext.SaveChangesAsync();
                return link;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAuthor(Author author)
        {
            try
            {
                await author.DoBeforeDelete(this);
                apiContext.Authors.Remove(author);
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAuthorBookLink(AuthorBookLink link)
        {
            try
            {
                apiContext.Remove(link);
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAuthorBookLinksForBook(int bookID)
        {
            try
            {
                apiContext.RemoveRange(apiContext.AuthorBookLinks.Where(x => x.BookID == bookID));
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteBook(Book book)
        {
            try
            {
                await book.DoBeforeDelete(this);
                apiContext.Books.Remove(book);
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteBookGenreLink(BookGenreLink link)
        {
            try
            {
                apiContext.Remove(link);
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteBookGenreLinksForBook(int bookID)
        {
            try
            {
                apiContext.RemoveRange(apiContext.BookGenreLinks.Where(x => x.BookID == bookID));
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteGenre(Genre genre)
        {
            try
            {
                await genre.DoBeforeDelete(this);
                apiContext.Genres.Remove(genre);
                return await apiContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByAuthorID(int authorID)
        {
            try
            {
                return await apiContext.AuthorBookLinks.Where(x => x.AuthorID == authorID).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByBookID(int bookID)
        {
            try
            {
                return await apiContext.AuthorBookLinks.Where(x => x.BookID == bookID).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Author> RetrieveAuthorByID(int authorID)
        {
            try
            {
                return await apiContext.Authors.FirstOrDefaultAsync(x => x.ID == authorID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Author>> RetrieveAuthors()
        {
            try
            {
                return await apiContext.Authors.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Author>> RetrieveAuthorsByBookID(int bookID)
        {
            try
            {
                return await apiContext.AuthorBookLinks.Where(a => a.BookID == bookID).Distinct().Select(x => x.Author).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Book> RetrieveBookByID(int bookID)
        {
            try
            {
                return await apiContext.Books.FirstOrDefaultAsync(x => x.ID == bookID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<BookGenreLink>> RetrieveBookGenreLinksByBookID(int bookID)
        {
            try
            {
                return await apiContext.BookGenreLinks.Where(x => x.BookID == bookID).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Book>> RetrieveBooks()
        {
            try
            {
                return await apiContext.Books.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Book>> RetrieveBooksByAuthorID(int authorID)
        {
            try
            {
                return await apiContext.AuthorBookLinks.Where(a => a.AuthorID == authorID).Distinct().Select(x => x.Book).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Book>> RetrieveBooksByGenreID(int genreID)
        {
            try
            {
                return await apiContext.BookGenreLinks.Where(a => a.GenreID == genreID).Distinct().Select(a => a.Book).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Genre> RetrieveGenreByID(int genreID)
        {
            try
            {
                return await apiContext.Genres.FirstOrDefaultAsync(x => x.ID == genreID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Genre>> RetrieveGenres()
        {
            try
            {

                return await apiContext.Genres.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<ICollection<Genre>> RetrieveGenresByBookID(int bookID)
        {
            try
            {
                return await apiContext.BookGenreLinks.Where(a => a.BookID == bookID).Distinct().Select(a => a.Genre).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            try
            {
                apiContext.Authors.Update(author);
                await apiContext.SaveChangesAsync();

                return author;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                apiContext.Books.Update(book);
                await apiContext.SaveChangesAsync();

                return book;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Genre> UpdateGenre(Genre genre)
        {
            try
            {
                apiContext.Genres.Update(genre);
                await apiContext.SaveChangesAsync();

                return genre;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}