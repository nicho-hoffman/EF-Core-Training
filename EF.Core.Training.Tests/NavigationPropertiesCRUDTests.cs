using EF.Core.Training.BlackBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.Threading.Tasks;

namespace EF.Core.Training.Tests
{
    [TestClass]
    public class NavigationPropertiesCRUDTests : TestsCommon
    {
        [TestMethod]
        public async Task TestDeletingBookWithBookGenreLinksDeletesBookAndAssociatedBookGenreLinksSucceeds()
        {
            var book = await CreateBook(TestBook);
            var genre = await CreateGenre(TestGenre);

            var testBookGenreLink = new BookGenreLink()
            {
                BookID = book.ID,
                GenreID = genre.ID
            };

            var createdLink = await repository.CreateBookGenreLink(testBookGenreLink);
            createdLink.Should().NotBeNull();
            createdLink.BookID.Should().Be(book.ID);
            createdLink.GenreID.Should().Be(genre.ID);

            await DeleteBook(book);

            var links = await repository.RetrieveBookGenreLinksByBookID(book.ID);
            links.Should().BeNullOrEmpty();

            var getGenre = await repository.RetrieveGenreByID(genre.ID);
            getGenre.Should().NotBeNull();
            getGenre.BookLinks.Should().BeNullOrEmpty();

            await DeleteGenre(getGenre);
        }

        [TestMethod]
        public async Task TestDeletingAuthorWithAuthorBookLinksFails()
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

            var deleteSuccessful = await repository.DeleteAuthor(author);
            deleteSuccessful.Should().BeFalse();

            await DeleteBook(book);

            var links = await repository.RetrieveAuthorBookLinksByAuthorID(author.ID);
            links.Should().BeNullOrEmpty();

            var getAuthor = await repository.RetrieveAuthorByID(author.ID);
            getAuthor.Should().NotBeNull();
            getAuthor.BookLinks.Should().BeNullOrEmpty();

            await DeleteAuthor(getAuthor);
        }

        [TestMethod]
        public async Task TestDeletingGenreWithBookGenreLinksFails()
        {
            var book = await CreateBook(TestBook);
            var genre = await CreateGenre(TestGenre);

            var testBookGenreLink = new BookGenreLink()
            {
                BookID = book.ID,
                GenreID = genre.ID
            };

            var createdLink = await repository.CreateBookGenreLink(testBookGenreLink);
            createdLink.Should().NotBeNull();
            createdLink.BookID.Should().Be(book.ID);
            createdLink.GenreID.Should().Be(genre.ID);

            var deleteSuccessful = await repository.DeleteGenre(genre);
            deleteSuccessful.Should().BeFalse();

            await DeleteBook(book);

            var links = await repository.RetrieveBookGenreLinksByBookID(book.ID);
            links.Should().BeNullOrEmpty();

            var getGenre = await repository.RetrieveGenreByID(genre.ID);
            getGenre.Should().NotBeNull();
            getGenre.BookLinks.Should().BeNullOrEmpty();

            await DeleteGenre(getGenre);
        }

        [TestMethod]
        public async Task TestUpdatingAuthorAuthorBookLinksSucceeds()
        {
            var author = await CreateAuthor(TestAuthor);
            var book = await CreateBook(TestBook);

            var testAuthorBookLink = new AuthorBookLink()
            {
                AuthorID = author.ID,
                BookID = book.ID,
            };

            var createdLink = await repository.CreateAuthorBookLink(testAuthorBookLink);
            createdLink.Should().NotBeNull();
            createdLink.AuthorID.Should().Be(author.ID);
            createdLink.BookID.Should().Be(book.ID);

            var bookTwo = await CreateBook(new Book()
            {
                ISBN = "9781250318572",
                Title = "The Well of Ascension",
                Pages = 816,
                Price = 10.99m,
                Description = "The impossible has been accomplished. The Lord Ruler — the man who claimed to be god incarnate and brutally ruled the world for a thousand years — has been vanquished."
            });

            author.BookLinks.Add(new AuthorBookLink()
            {
                AuthorID = author.ID,
                BookID = bookTwo.ID
            });

            // test add a link
            var updatedAuthor = await repository.UpdateAuthor(author);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.BookLinks.Should().NotBeNullOrEmpty();
            updatedAuthor.BookLinks.Count.Should().Be(2);

            // test remove a link
            updatedAuthor.BookLinks.Remove(createdLink);
            updatedAuthor = await repository.UpdateAuthor(updatedAuthor);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.BookLinks.Should().NotBeNullOrEmpty();
            updatedAuthor.BookLinks.Count.Should().Be(1);
            updatedAuthor.BookLinks.Should().NotContain(x => x.BookID == book.ID);

            await DeleteBook(book);
            await DeleteBook(bookTwo);
            await DeleteAuthor(author);
        }

        [TestMethod]
        public async Task TestUpdatingBookBookGenreLinksSucceeds()
        {
            var book = await CreateBook(TestBook);
            var genre = await CreateGenre(TestGenre);

            var testBookGenreLink = new BookGenreLink()
            {
                BookID = book.ID,
                GenreID = genre.ID
            };

            var createdLink = await repository.CreateBookGenreLink(testBookGenreLink);
            createdLink.Should().NotBeNull();
            createdLink.BookID.Should().Be(book.ID);
            createdLink.GenreID.Should().Be(genre.ID);

            var genreTwo = await CreateGenre(new Genre()
            {
                Name = "Sci Fi"
            });

            book.GenreLinks.Add(new BookGenreLink()
            {
                BookID = book.ID,
                GenreID = genreTwo.ID
            });

            // test add a link
            var updatedBook = await repository.UpdateBook(book);
            updatedBook.Should().NotBeNull();
            updatedBook.GenreLinks.Should().NotBeNullOrEmpty();
            updatedBook.GenreLinks.Count.Should().Be(2);

            // test remove a link
            updatedBook.GenreLinks.Remove(createdLink);
            updatedBook = await repository.UpdateBook(updatedBook);
            updatedBook.Should().NotBeNull();
            updatedBook.GenreLinks.Should().NotBeNullOrEmpty();
            updatedBook.GenreLinks.Count.Should().Be(1);
            updatedBook.GenreLinks.Should().NotContain(x => x.GenreID == genre.ID);

            await DeleteBook(book);
        }
    }
}
