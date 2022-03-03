using EF.Core.Training.BlackBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Threading.Tasks;

namespace EF.Core.Training.Tests
{
    public class TestsCommon
    {
        protected IRepository repository;

        protected Author TestAuthor = new()
        {
            Name = "Brandon Sanderson",
            First = "Brandon",
            Last = "Sanderson",
            Bio = "An American author of epic fantasy and science fiction."
        };

        protected Book TestBook = new ()
        {
            ISBN = "9781250318541",
            Title = "Mistborn: The Final Empire",
            Pages = 672,
            Price = 8.99m,
            Description = "For a thousand years, the ash has fallen and no flowers have bloomed."
        };

        protected Genre TestGenre = new ()
        {
            Name = "High Fantasy"
        };

        [TestInitialize]
        public async Task Initialize()
        {
            repository = new EfRepository();
            await repository.MigrateDb();
        }

        public async Task<Author> CreateAuthor(Author author = null)
        {
            if (author == null)
                author = TestAuthor;

            var createdAuthor = await repository.CreateAuthor(author);
            createdAuthor.Should().NotBeNull();
            createdAuthor.ID.Should().NotBe(0);
            createdAuthor.Name.Should().Be(author.Name);
            createdAuthor.First.Should().Be(author.First);
            createdAuthor.Last.Should().Be(author.Last);
            createdAuthor.Bio.Should().Be(author.Bio);

            return createdAuthor;
        }

        public async Task DeleteAuthor(Author author)
        {
            var deleteSuccessful = await repository.DeleteAuthor(author);
            deleteSuccessful.Should().BeTrue();

            author = await repository.RetrieveAuthorByID(author.ID);
            author.Should().BeNull();
        }

        public async Task<Book> CreateBook(Book book = null)
        {
            if (book == null)
                book = TestBook;

            var createdBook = await repository.CreateBook(book);
            createdBook.Should().NotBeNull();
            createdBook.ID.Should().NotBe(0);
            createdBook.ISBN.Should().Be(book.ISBN);
            createdBook.Title.Should().Be(book.Title);
            createdBook.Description.Should().Be(book.Description);
            createdBook.Pages.Should().Be(book.Pages);
            createdBook.Price.Should().Be(book.Price);

            return createdBook;
        }

        public async Task DeleteBook(Book book)
        {
            var deleteSuccessful = await repository.DeleteBook(book);
            deleteSuccessful.Should().BeTrue();

            book = await repository.RetrieveBookByID(book.ID);
            book.Should().BeNull();
        }

        public async Task<Genre> CreateGenre(Genre genre = null)
        {
            if (genre == null)
                genre = TestGenre;

            var createdGenre = await repository.CreateGenre(genre);
            createdGenre.Should().NotBeNull();
            createdGenre.ID.Should().NotBe(0);
            createdGenre.Name.Should().Be(genre.Name);

            return createdGenre;
        }

        public async Task DeleteGenre(Genre genre)
        {
            var deleteSuccessful = await repository.DeleteGenre(genre);
            deleteSuccessful.Should().BeTrue();

            genre = await repository.RetrieveGenreByID(genre.ID);
            genre.Should().BeNull();
        }
    }
}
