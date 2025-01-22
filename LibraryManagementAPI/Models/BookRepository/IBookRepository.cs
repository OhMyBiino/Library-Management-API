using LibraryManagementModels;

namespace LibraryManagementAPI.Models.BookRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> GetBooksAsync();
        Task<BookModel> GetBookByIdAsync(int Id);
        Task<IEnumerable<BookModel>> SearchAsync(string Query, string Genre);
        Task<BookModel> CreateBookAsync(BookModel bookModel);
        Task<BookModel> UpdateBookAsync(BookModel bookModel);
        Task<BookModel> DeleteBookAsync(int Id);
    }
}
