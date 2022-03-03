
namespace EF.Core.Training.BlackBox
{
    public class Book
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<AuthorBookLink> AuthorLinks { get; set; }
        public virtual ICollection<BookGenreLink> GenreLinks { get; set; }

        public async Task DoBeforeDelete(IRepository repository)
        {
            if (AuthorLinks != null && AuthorLinks.Any())
                await repository.DeleteAuthorBookLinksForBook(ID);
            if (GenreLinks != null && GenreLinks.Any())
                await repository.DeleteBookGenreLinksForBook(ID);
        }
    }
}
