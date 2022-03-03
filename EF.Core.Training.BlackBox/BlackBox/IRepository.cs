
namespace EF.Core.Training.BlackBox
{
    /// <summary>
    /// IRepository for CRUD operations.
    /// Do NOT alter this file!
    /// Repository changes should all take place in <see cref="EfRepository"/>.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Runs migrations for the Database
        /// </summary>
        /// <returns></returns>
        Task MigrateDb();

        #region Create
        /// <summary>
        /// Create a new <see cref="Author"/>
        /// </summary>
        /// <param name="author"><see cref="Author"/> entity</param>
        /// <returns>Created <see cref="Author"/> with its generated ID</returns>
        Task<Author> CreateAuthor(Author author);

        /// <summary>
        /// Create a new <see cref="AuthorBookLink"/>
        /// </summary>
        /// <param name="link"><see cref="AuthorBookLink"/> entity</param>
        /// <returns>Created <see cref="AuthorBookLink"/> with its generated ID</returns>
        Task<AuthorBookLink> CreateAuthorBookLink(AuthorBookLink link);

        /// <summary>
        /// Create a new <see cref="BookGenreLink"/>
        /// </summary>
        /// <param name="link"><see cref="BookGenreLink"/> entity</param>
        /// <returns>Created <see cref="BookGenreLink"/> with its generated ID</returns>
        Task<BookGenreLink> CreateBookGenreLink(BookGenreLink link);

        /// <summary>
        /// Create a new <see cref="Book"/>
        /// </summary>
        /// <param name="book"><see cref="Book"/> entity</param>
        /// <returns>Created <see cref="Book"/> with its generated ID</returns>
        Task<Book> CreateBook(Book book);

        /// <summary>
        /// Create a new <see cref="Genre"/>
        /// </summary>
        /// <param name="book"><see cref="Genre"/> entity</param>
        /// <returns>Created <see cref="Genre"/> with its generated ID</returns>
        Task<Genre> CreateGenre(Genre genre);
        #endregion

        #region Retrieve
        /// <summary>
        /// Gets an array of all <see cref="Author"/>s
        /// </summary>
        /// <returns><see cref="Author"/> array</returns>
        Task<ICollection<Author>> RetrieveAuthors();

        /// <summary>
        /// Gets an <see cref="Author"/> by its ID
        /// </summary>
        /// <param name="authorID"><see cref="Author"/>'s ID</param>
        /// <returns><see cref="Author"/> entity</returns>
        Task<Author> RetrieveAuthorByID(int authorID);

        /// <summary>
        /// Gets an array of <see cref="Author"/> by BookID
        /// </summary>
        /// <returns><see cref="Author"/> array</returns>
        Task<ICollection<Author>> RetrieveAuthorsByBookID(int bookID);

        /// <summary>
        /// Gets an array of <see cref="AuthorBookLink"/> by AuthorID
        /// </summary>
        /// <param name="authorID"><see cref="Author"/>'s ID</param>
        /// <returns><see cref="AuthorBookLink"/> array</returns>
        Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByAuthorID(int authorID);

        /// <summary>
        /// Gets an array of <see cref="AuthorBookLink"/> by BookID
        /// </summary>
        /// <param name="bookID"><see cref="Book"/>'s ID</param>
        /// <returns><see cref="AuthorBookLink"/> array</returns>
        Task<ICollection<AuthorBookLink>> RetrieveAuthorBookLinksByBookID(int bookID);

        /// <summary>
        /// Gets an array of <see cref="BookGenreLink"/> by BookID
        /// </summary>
        /// <param name="bookID"><see cref="Book"/>'s ID</param>
        /// <returns><see cref="BookGenreLink"/> array</returns>
        Task<ICollection<BookGenreLink>> RetrieveBookGenreLinksByBookID(int bookID);

        /// <summary>
        /// Gets an array of all <see cref="Book"/>s
        /// </summary>
        /// <returns><see cref="Book"/> array</returns>
        Task<ICollection<Book>> RetrieveBooks();

        /// <summary>
        /// Gets an <see cref="Book"/> by its ID
        /// </summary>
        /// <param name="bookID"><see cref="Book"/>'s ID</param>
        /// <returns><see cref="Book"/> entity</returns>
        Task<Book> RetrieveBookByID(int bookID);

        /// <summary>
        /// Gets an array of <see cref="Book"/> by BookID
        /// </summary>
        /// <param name="authorID"><see cref="Author"/>'s ID</param>
        /// <returns><see cref="Book"/> array</returns>
        Task<ICollection<Book>> RetrieveBooksByAuthorID(int authorID);

        /// <summary>
        /// Gets an array of <see cref="Book"/> by BookID
        /// </summary>
        /// <param name="genreID"><see cref="Genre"/>'s ID</param>
        /// <returns><see cref="Book"/> array</returns>
        Task<ICollection<Book>> RetrieveBooksByGenreID(int genreID);

        /// <summary>
        /// Gets an array of all <see cref="Genre"/>s
        /// </summary>
        /// <returns><see cref="Genre"/> array</returns>
        Task<ICollection<Genre>> RetrieveGenres();

        /// <summary>
        /// Gets an <see cref="Genre"/> by its ID
        /// </summary>
        /// <param name="genreID"></param>
        /// <returns><see cref="Genre"/></returns>
        Task<Genre> RetrieveGenreByID(int genreID);

        /// <summary>
        /// Gets an array of <see cref="Genre"/> by BookID
        /// </summary>
        /// <param name="bookID"><see cref="Book"/>'s ID</param>
        /// <returns><see cref="Genre"/> array</returns>
        Task<ICollection<Genre>> RetrieveGenresByBookID(int bookID);
        #endregion

        #region Update
        /// <summary>
        /// Updates an <see cref="Author"/>
        /// </summary>
        /// <param name="author">Updated <see cref="Author"/></param>
        /// <returns></returns>
        Task<Author> UpdateAuthor(Author author);

        /// <summary>
        /// Updates an <see cref="Book"/>
        /// </summary>
        /// <param name="book">Updated <see cref="Book"/></param>
        /// <returns></returns>
        Task<Book> UpdateBook(Book author);

        /// <summary>
        /// Updates an <see cref="Genre"/>
        /// </summary>
        /// <param name="genre">Updated <see cref="Genre"/></param>
        /// <returns></returns>
        Task<Genre> UpdateGenre(Genre genre);
        #endregion

        #region Delete
        /// <summary>
        /// Deletes an <see cref="Author"/>
        /// </summary>
        /// <param name="author"><see cref="Author"/> to delete</param>
        /// <returns>True if the <see cref="Author"/> successfully deleted.</returns>
        Task<bool> DeleteAuthor(Author author);

        /// <summary>
        /// Deletes an <see cref="AuthorBookLink"/>
        /// </summary>
        /// <param name="link"><see cref="AuthorBookLink"/> to delete</param>
        /// <returns>True if the <see cref="AuthorBookLink"/> successfully deleted.</returns>
        Task<bool> DeleteAuthorBookLink(AuthorBookLink link);

        /// <summary>
        /// Deletes all of the <see cref="AuthorBookLink"/>s for the given BookID
        /// </summary>
        /// <param name="bookID"><see cref="Book"/> ID</param>
        /// <returns>True if all of the <see cref="AuthorBookLink"/>s successfully deleted.</returns>
        Task<bool> DeleteAuthorBookLinksForBook(int bookID);

        /// <summary>
        /// Deletes an <see cref="BookGenreLink"/>
        /// </summary>
        /// <param name="link"><see cref="BookGenreLink"/> to delete</param>
        /// <returns>True if the <see cref="BookGenreLink"/> successfully deleted.</returns>
        Task<bool> DeleteBookGenreLink(BookGenreLink link);

        /// <summary>
        /// Deletes all of the <see cref="BookGenreLink"/>s for the given BookID
        /// </summary>
        /// <param name="bookID"><see cref="Book"/> ID</param>
        /// <returns>True if all of the <see cref="BookGenreLink"/>s successfully deleted.</returns>
        Task<bool> DeleteBookGenreLinksForBook(int bookID);

        /// <summary>
        /// Deletes an <see cref="Book"/>
        /// </summary>
        /// <param name="book"><see cref="Book"/> to delete</param>
        /// <returns>True if the <see cref="Book"/> successfully deleted.</returns>
        Task<bool> DeleteBook(Book book);

        /// <summary>
        /// Deletes an <see cref="Genre"/>
        /// </summary>
        /// <param name="genre"><see cref="Genre"/> to delete</param>
        /// <returns>True if the <see cref="Genre"/> successfully deleted.</returns>
        Task<bool> DeleteGenre(Genre genre);
        #endregion
    }
}
