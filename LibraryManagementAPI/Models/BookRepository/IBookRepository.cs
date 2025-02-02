﻿using LibraryManagementModels;

namespace LibraryManagementAPI.Models.BookRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> GetBooksAsync();
        Task<BookModel> GetBookByIdAsync(int Id);
        Task<IEnumerable<BookModel>> GetBorrowedBooksAsync();
        Task<IEnumerable<BookModel>> SearchAsync(string Name, string Genre);
        Task<BookModel> CreateBookAsync(BookModel bookModel);
        Task<BookModel> UpdateBookAsync(BookModel bookModel);
        Task<BookModel> DeleteBookAsync(int Id);
    }
}
