using BookStoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApi.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int bookId);

        Task<int> AddBookAsync(BookModel book);

        Task UpdateBookAsync(int bookId, BookModel bookModel);
        Task DeleteBookAync(int bookId);
    }
}
