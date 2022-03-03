
namespace EF.Core.Training.BlackBox
{
    public class AuthorBookLink
    {
        #region Do Not Alter
        public int AuthorID { get; set; }
        public int BookID { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
        #endregion
    }
}
