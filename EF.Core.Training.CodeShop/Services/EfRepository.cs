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
            this.apiContext.Authors.Add(author);
            await this.apiContext.SaveChangesAsync();
            return author;
        }

        public async Task<AuthorBookLink> CreateAuthorBookLink(AuthorBookLink link)
        {
            this.apiContext.AuthorBookLinks.Add(link);
            await this.apiContext.SaveChangesAsync();
            return link;
        }

        public async Task<Book> CreateBook(Book book)
        {
            this.apiContext.Books.Add(book);
            await this.apiContext.SaveChangesAsync();
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
            this.apiContext.Genres.Add(genre);
            await this.apiContext.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> DeleteAuthor(Author author)
        {
            try
            {
                await author.DoBeforeDelete(this);
                this.apiContext.Authors.Remove(author);
                return await this.apiContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAuthorBookLink(AuthorBookLink link)
        {
            this.apiContext.Remove(link);
            return await apiContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAuthorBookLinksForBook(int bookID)
        {
            this.apiContext.RemoveRange(apiContext.AuthorBookLinks.Where(x => x.BookID == bookID));
            return await apiContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBook(Book book)
        {
            try
            {
                await book.DoBeforeDelete(this);
                this.apiContext.Books.Remove(book);
                return await this.apiContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
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
                return false;
            }
        }

        public async Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByAuthorID(int authorID)
        {
            return await this.apiContext.AuthorBookLinks.Where(x => x.AuthorID == authorID).ToListAsync();
        }

        public async Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByBookID(int bookID)
        {
            return await this.apiContext.AuthorBookLinks.Where(x => x.BookID == bookID).ToListAsync();
        }

        public async Task<Author> RetrieveAuthorByID(int authorID)
        {
            return await this.apiContext.Authors.FirstOrDefaultAsync(x => x.ID == authorID);
        }

        public async Task<ICollection<Author>> RetrieveAuthors()
        {
            return await this.apiContext.Authors.ToListAsync();
        }

        public async Task<ICollection<Author>> RetrieveAuthorsByBookID(int bookID)
        {
            return await this.apiContext.Authors
                .Include(x => x.BookLinks)
                .Where(x => x.BookLinks.Any(y => y.BookID == bookID))
                .ToListAsync();
        }

        public async Task<Book> RetrieveBookByID(int bookID)
        {
            return await this.apiContext.Books.FirstOrDefaultAsync(x => x.ID == bookID);
        }

        public async Task<ICollection<BookGenreLink>> RetrieveBookGenreLinksByBookID(int bookID)
        {
            return await this.apiContext.BookGenreLinks.Where(x => x.BookID == bookID).ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooks()
        {
            return await this.apiContext.Books.ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooksByAuthorID(int authorID)
        {
            return await this.apiContext.Books
                 .Include(x => x.AuthorLinks)
                 .Where(x => x.AuthorLinks.Any(y => y.AuthorID == authorID))
                 .ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooksByGenreID(int genreID)
        {
            return await this.apiContext.Books
                .Include(x => x.GenreLinks)
                .Where(x => x.GenreLinks.Any(y => y.GenreID == genreID))
                .ToListAsync();
        }

        public async Task<Genre> RetrieveGenreByID(int genreID)
        {
            return await this.apiContext.Genres.FirstOrDefaultAsync(x => x.ID == genreID);
        }

        public async Task<ICollection<Genre>> RetrieveGenres()
        {
            return await this.apiContext.Genres.ToListAsync();
        }

        public async Task<ICollection<Genre>> RetrieveGenresByBookID(int bookID)
        {
            return await this.apiContext.Genres.Include(x => x.BookLinks)
                .Where(x => x.BookLinks.Any(l => l.BookID == bookID)).ToListAsync();
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            var data = await this.apiContext.Authors.FirstOrDefaultAsync(x => x.ID == author.ID);
            if (data == null)
            {
                throw new EntryPointNotFoundException();
            }
            data.Name = author.Name;
            data.First = author.First;
            data.Last = author.Last;
            data.Bio = author.Bio;
            data.BookLinks = author.BookLinks;
            await this.apiContext.SaveChangesAsync();

            return data;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var data = await this.apiContext.Books.FirstOrDefaultAsync(x => x.ID == book.ID);
            if (data == null)
            {
                throw new EntryPointNotFoundException();
            }
            data.ISBN = book.ISBN;
            data.Title = book.Title;
            data.Description = book.Description;
            data.Pages = book.Pages;
            data.Price = book.Price;
            data.AuthorLinks = book.AuthorLinks;
            data.GenreLinks = book.GenreLinks;
            await this.apiContext.SaveChangesAsync();

            return data;
        }

        public async Task<Genre> UpdateGenre(Genre genre)
        {
            var data = await this.apiContext.Genres.FirstOrDefaultAsync(x => x.ID == genre.ID);
            if (data == null)
            {
                throw new EntryPointNotFoundException();
            }
            data.Name = genre.Name;
            data.BookLinks = genre.BookLinks;
            await apiContext.SaveChangesAsync();

            return data;
        }
    }
}
