
namespace EF.Core.Training.BlackBox
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<AuthorBookLink> BookLinks { get; set; }

        public async Task DoBeforeDelete(IRepository repository)
        {
            if (BookLinks != null && BookLinks.Any())
                throw new InvalidOperationException("Cannot delete an Author that has Books associated with it.");

            await Task.Yield();
        }
    }
}
