using LibraryManagementAPI.Database;
using LibraryManagementModels;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementAPI.Models.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<BookModel>> GetBooksAsync() 
        {
            var books = await _context.Books.ToListAsync();

            return books;
        }

        public async Task<BookModel> GetBookByIdAsync(int Id) 
        {
            var book = await _context.Books.FindAsync(Id);

            return book;
        }

        public async Task<IEnumerable<BookModel>> SearchAsync(string Name, string Genre) 
        {
            IQueryable<BookModel> query = _context.Books;

            if (!String.IsNullOrEmpty(Name)) 
            {
                query = query.Where(b => b.Title.Contains(Name) || b.ISBN.Contains(Name) || b.Author.Contains(Name));
            }

            if (Genre != null) 
            {
                query = query.Where(b => b.Genre.Contains(Genre));
            }

            return query.ToList();
        }

        public async Task<BookModel> CreateBookAsync(BookModel bookModel) 
        {
            var addedBook = await _context.Books.AddAsync(bookModel);

            await _context.SaveChangesAsync();
            return addedBook.Entity;
        }

        public async Task<BookModel> UpdateBookAsync(BookModel updatedBook) 
        {
            var bookToUpdate = await _context.Books.FindAsync(updatedBook.BookId);

            if (bookToUpdate != null) 
            {
                bookToUpdate.ISBN = updatedBook.ISBN;
                bookToUpdate.Title = updatedBook.Title;
                bookToUpdate.Genre = updatedBook.Genre;
                bookToUpdate.Description = updatedBook.Description;
                bookToUpdate.Author = updatedBook.Author;
                bookToUpdate.isBorrowed = updatedBook.isBorrowed;
            }

            await _context.SaveChangesAsync();
            return bookToUpdate;
        }

        public async Task<BookModel> DeleteBookAsync(int Id) 
        {
            var bookToDelete = await _context.Books.FindAsync(Id);


            if (bookToDelete != null) 
            {
                _context.Books.Remove(bookToDelete);
                await _context.SaveChangesAsync();
            }

            return bookToDelete;
        }
    }
}
