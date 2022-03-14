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
            apiContext.Authors.Add(author);
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
            var author1 = await apiContext.Authors.FirstOrDefaultAsync(x => x.ID == author.ID);
            if (author1 != null)
            {
                try
                {
                    await author1.DoBeforeDelete(this);
                    apiContext.Authors.Remove(author);
                    await apiContext.SaveChangesAsync();
                    return true;
                }catch (Exception ex)
                {
                    return false;
                }
                
            }

            return false;
        }

        public async Task<bool> DeleteAuthorBookLink(AuthorBookLink link)
        {
            if(await apiContext.AuthorBookLinks.Where(e => e.AuthorID == link.AuthorID && e.BookID == link.BookID).AnyAsync())
            {
                apiContext.AuthorBookLinks.Remove(link);
                await apiContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAuthorBookLinksForBook(int bookID)
        {
            var query = apiContext.AuthorBookLinks.Where(e => e.BookID == bookID);
            if(await query.AnyAsync())
            {
                var list = await query.ToListAsync();
                apiContext.AuthorBookLinks.RemoveRange(list);
                await apiContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBook(Book book)
        {
            //throw new NotImplementedException();

            var bk = await apiContext.Books.FirstOrDefaultAsync(x => x.ID == book.ID);
            if (bk != null)
            {
                try
                {

                    await bk.DoBeforeDelete(this);
                    apiContext.Books.Remove(book);
                    await apiContext.SaveChangesAsync();
                    return true;
                }catch(Exception ex)
                {
                    return false;
                }
            }

            return false;



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
            //throw new NotImplementedException();

            return await apiContext.AuthorBookLinks.Where(x => x.AuthorID == authorID)
                .ToListAsync();
        }

        public async Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByBookID(int bookID)
        {
            //throw new NotImplementedException();
            return await apiContext.AuthorBookLinks.Where(x => x.BookID == bookID)
                .ToListAsync();
        }

        public async Task<Author> RetrieveAuthorByID(int authorID)
        {
            return await apiContext.Authors.Where(x => x.ID == authorID).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Author>> RetrieveAuthors()
        {
            return await apiContext.Authors.ToListAsync();
        }

        public async Task<ICollection<Author>> RetrieveAuthorsByBookID(int bookID)
        {
            return await apiContext.Authors.Include(e => e.BookLinks).Where(e => e.BookLinks.Where(e => e.BookID == bookID).Any()).ToListAsync();
        }

        public async Task<Book> RetrieveBookByID(int bookID)
        {
            //throw new NotImplementedException();
            return await apiContext.Books.Where(b => b.ID == bookID).SingleOrDefaultAsync();
        }

        public async Task<ICollection<BookGenreLink>> RetrieveBookGenreLinksByBookID(int bookID)
        {
            return await apiContext.BookGenreLinks.Where(x => x.BookID == bookID).ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooks()
        {
            //throw new NotImplementedException();
            return await apiContext.Books.ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooksByAuthorID(int authorID)
        {
            return await apiContext.Books.Include(x => x.AuthorLinks).Where(x => x.AuthorLinks.Where(x => x.AuthorID == authorID).Any()).ToListAsync();
        }

        public async Task<ICollection<Book>> RetrieveBooksByGenreID(int genreID)
        {
            //throw new NotImplementedException();
            return await apiContext.Books.Include(x => x.GenreLinks)
                .Where(x => x.GenreLinks.Any(l => l.GenreID == genreID)).ToListAsync();
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
            var author1 = apiContext.Authors.Where(e => e.ID == author.ID).FirstOrDefault();
            author1 = author;
            await apiContext.SaveChangesAsync();
            return author1;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            //throw new NotImplementedException();
            var bk = await apiContext.Books.FirstOrDefaultAsync(x => x.ID == book.ID);
            bk = book;
            await apiContext.SaveChangesAsync();
            return bk;
        }

        public async Task<Genre> UpdateGenre(Genre genre)
        {
            apiContext.Genres.Update(genre);
            await apiContext.SaveChangesAsync();

            return genre;
        }
    }
}
