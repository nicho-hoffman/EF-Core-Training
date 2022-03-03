
namespace EF.Core.Training.BlackBox
{
    public class BookGenreLink
    {
        public int BookID { get; set; }
        public int GenreID { get; set; }
        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
