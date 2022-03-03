using EF.Core.Training.BlackBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.Threading.Tasks;

namespace EF.Core.Training.Tests
{
    [TestClass]
    public class CRUDTests : TestsCommon
    {
        [TestMethod]
        public async Task AuthorCRUDTest()
        {
            var createdAuthor = await CreateAuthor(TestAuthor);

            var retrievedAuthor = await repository.RetrieveAuthorByID(createdAuthor.ID);
            retrievedAuthor.Should().NotBeNull();

            var retrievedAuthors = await repository.RetrieveAuthors();
            retrievedAuthors.Should().NotBeNull();
            retrievedAuthors.Should().Contain(retrievedAuthor);

            var newName = TestAuthor.Name + DateTime.UtcNow.ToString();
            createdAuthor.Name = newName;
            var updatedAuthor = await repository.UpdateAuthor(createdAuthor);
            updatedAuthor.Should().NotBeNull();
            createdAuthor.ID.Should().NotBe(0);
            updatedAuthor.Name.Should().Be(newName);
            updatedAuthor.First.Should().Be(TestAuthor.First);
            updatedAuthor.Last.Should().Be(TestAuthor.Last);
            updatedAuthor.Bio.Should().Be(TestAuthor.Bio);

            retrievedAuthor = await repository.RetrieveAuthorByID(createdAuthor.ID);
            retrievedAuthor.Should().NotBeNull();
            retrievedAuthor.Name.Should().Be(newName);

            await DeleteAuthor(retrievedAuthor);
        }

        [TestMethod]
        public async Task BookCRUDTest()
        {
            var createdBook = await CreateBook(TestBook);

            var retrievedBook = await repository.RetrieveBookByID(createdBook.ID);
            retrievedBook.Should().NotBeNull();

            var retrievedBooks = await repository.RetrieveBooks();
            retrievedBooks.Should().NotBeNull();
            retrievedBooks.Should().Contain(retrievedBook);

            var newPrice = TestBook.Price * 0.5m;
            createdBook.Price = newPrice;
            var updatedBook = await repository.UpdateBook(createdBook);
            updatedBook.Should().NotBeNull();
            createdBook.ID.Should().NotBe(0);
            updatedBook.Price.Should().Be(newPrice);

            retrievedBook = await repository.RetrieveBookByID(createdBook.ID);
            retrievedBook.Should().NotBeNull();
            retrievedBook.Price.Should().Be(newPrice);

            await DeleteBook(retrievedBook);
        }

        [TestMethod]
        public async Task AuthorBookLinkCRUDTest()
        {
            var author = await CreateAuthor(TestAuthor);
            var book = await CreateBook(TestBook);

            var testAuthorBookLink = new AuthorBookLink()
            {
                AuthorID = author.ID,
                BookID = book.ID
            };

            var createdLink = await repository.CreateAuthorBookLink(testAuthorBookLink);
            createdLink.Should().NotBeNull();
            createdLink.AuthorID.Should().Be(author.ID);
            createdLink.BookID.Should().Be(book.ID);

            var authorBookLinks = await repository.RetrieveAuthorBookLinksByAuthorID(createdLink.AuthorID);
            authorBookLinks.Should().NotBeNullOrEmpty();
            authorBookLinks.Count.Should().Be(1);
            authorBookLinks.Should().Contain(x => x.AuthorID == author.ID);
            authorBookLinks.Should().Contain(x => x.BookID == book.ID);

            authorBookLinks = await repository.RetrieveAuthorBookLinksByBookID(createdLink.BookID);
            authorBookLinks.Should().NotBeNullOrEmpty();
            authorBookLinks.Count.Should().Be(1);
            authorBookLinks.Should().Contain(x => x.AuthorID == author.ID);
            authorBookLinks.Should().Contain(x => x.BookID == book.ID);

            var linkedBooks = await repository.RetrieveBooksByAuthorID(createdLink.AuthorID);
            linkedBooks.Should().NotBeNullOrEmpty();
            linkedBooks.Count.Should().BeGreaterThanOrEqualTo(1);
            linkedBooks.Should().Contain(x => x.ISBN.Equals(TestBook.ISBN));

            var linkedAuthors = await repository.RetrieveAuthorsByBookID(createdLink.BookID);
            linkedAuthors.Should().NotBeNullOrEmpty();
            linkedAuthors.Count.Should().BeGreaterThanOrEqualTo(1);
            linkedAuthors.Should().Contain(x => x.Name.Equals(TestAuthor.Name));

            var deleteSuccessful = await repository.DeleteAuthorBookLink(createdLink);
            deleteSuccessful.Should().BeTrue();

            authorBookLinks = await repository.RetrieveAuthorBookLinksByBookID(author.ID);
            authorBookLinks.Should().BeNullOrEmpty();

            await DeleteAuthor(await repository.RetrieveAuthorByID(author.ID));
            await DeleteBook(await repository.RetrieveBookByID(book.ID));
        }

        [TestMethod]
        public async Task GenreCRUDTest()
        {
            var createdGenre = await CreateGenre(TestGenre);

            var retrievedGenre = await repository.RetrieveGenreByID(createdGenre.ID);
            retrievedGenre.Should().NotBeNull();

            var retrievedGenres = await repository.RetrieveGenres();
            retrievedGenres.Should().NotBeNull();
            retrievedGenres.Should().Contain(retrievedGenre);

            var newName = TestGenre.Name + DateTime.UtcNow.ToString();
            createdGenre.Name = newName;
            var updatedGenre = await repository.UpdateGenre(createdGenre);
            updatedGenre.Should().NotBeNull();
            createdGenre.ID.Should().NotBe(0);
            updatedGenre.Name.Should().Be(newName);

            retrievedGenre = await repository.RetrieveGenreByID(createdGenre.ID);
            retrievedGenre.Should().NotBeNull();
            retrievedGenre.Name.Should().Be(newName);

            await DeleteGenre(retrievedGenre);
        }

        [TestMethod]
        public async Task BookGenreLinkCRUDTest()
        {
            var createdBook = await CreateBook(TestBook);
            var createdGenre = await CreateGenre(TestGenre);

            var testBookGenreLink = new BookGenreLink()
            {
                BookID = createdBook.ID,
                GenreID = createdGenre.ID
            };

            var createdLink = await repository.CreateBookGenreLink(testBookGenreLink);
            createdLink.Should().NotBeNull();
            createdLink.BookID.Should().Be(createdBook.ID);
            createdLink.GenreID.Should().Be(createdGenre.ID);

            var bookGenreLinks = await repository.RetrieveBookGenreLinksByBookID(createdLink.BookID);
            bookGenreLinks.Should().NotBeNullOrEmpty();
            bookGenreLinks.Count.Should().Be(1);
            bookGenreLinks.Should().Contain(x => x.BookID == createdBook.ID);

            var linkedBook = await repository.RetrieveGenresByBookID(createdLink.BookID);
            linkedBook.Should().NotBeNullOrEmpty();
            linkedBook.Count.Should().BeGreaterThanOrEqualTo(1);
            linkedBook.Should().Contain(x => x.Name.Equals(TestGenre.Name));

            var linkedAuthor = await repository.RetrieveBooksByGenreID(createdLink.GenreID);
            linkedAuthor.Should().NotBeNullOrEmpty();
            linkedAuthor.Count.Should().BeGreaterThanOrEqualTo(1);
            linkedAuthor.Should().Contain(x => x.ISBN.Equals(TestBook.ISBN));

            var deleteSuccessful = await repository.DeleteBookGenreLink(createdLink);
            deleteSuccessful.Should().BeTrue();

            bookGenreLinks = await repository.RetrieveBookGenreLinksByBookID(createdLink.BookID);
            bookGenreLinks.Should().BeNullOrEmpty();

            await DeleteBook(await repository.RetrieveBookByID(createdBook.ID));
            await DeleteGenre(await repository.RetrieveGenreByID(createdGenre.ID));
        }

        [TestMethod]
        public async Task AuthorWithNullNameFailsTest()
        {
            try
            {
                var author = TestAuthor;
                author.Name = null;
                await repository.CreateAuthor(author);
                author.Should().BeNull();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.InnerException.Should().NotBeNull();
                ex.InnerException.Message.Should().Be("SQLite Error 19: 'NOT NULL constraint failed: Author.Name'.");
            }
        }

        [TestMethod]
        public async Task BookWithNullISBNFailsTest()
        {
            try
            {
                var book = TestBook;
                book.ISBN = null;
                await repository.CreateBook(book);
                book.Should().BeNull();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.InnerException.Should().NotBeNull();
                ex.InnerException.Message.Should().Be("SQLite Error 19: 'NOT NULL constraint failed: Book.ISBN'.");
            }
        }

        [TestMethod]
        public async Task BookWithNullTitleFailsTest()
        {
            try
            {
                var book = TestBook;
                book.Title = null;
                await repository.CreateBook(book);
                book.Should().BeNull();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.InnerException.Should().NotBeNull();
                ex.InnerException.Message.Should().Be("SQLite Error 19: 'NOT NULL constraint failed: Book.Title'.");
            }
        }

        [TestMethod]
        public async Task GenreWithNullNameFailsTest()
        {
            try
            {
                var genre = new Genre();
                await repository.CreateGenre(genre);
                genre.Should().BeNull();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.InnerException.Should().NotBeNull();
                ex.InnerException.Message.Should().Be("SQLite Error 19: 'NOT NULL constraint failed: Genre.Name'.");
            }

        }
    }
}