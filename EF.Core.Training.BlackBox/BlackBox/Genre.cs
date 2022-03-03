
namespace EF.Core.Training.BlackBox
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookGenreLink> BookLinks { get; set; }

        public async Task DoBeforeDelete(IRepository repository)
        {
            if (BookLinks != null && BookLinks.Any())
                throw new InvalidOperationException("Cannot delete a Genre that has Books associated with it.");

            await Task.Yield();
        }
    }
}
