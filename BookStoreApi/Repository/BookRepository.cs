using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }


        public async Task<List<BookModel>> GetAllBooksAsync() {

            var records = await _context.Books.Select(x=> new BookModel() {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author
            
            }).ToListAsync();

            return records;
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            var records = await _context.Books.Where(a => a.Id == bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author

            }).FirstOrDefaultAsync();

            return records;
        }


        public async Task<int> AddBookAsync(BookModel book) {
            var obj = new Books() { Title = book.Title, Author = book.Author };
            _context.Books.Add(obj);
            await _context.SaveChangesAsync();
            return obj.Id;
            
        }


        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
           var book =  await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.Title = bookModel.Title;
                book.Author = bookModel.Author;
                await _context.SaveChangesAsync();
            
            }

        }

        public async Task DeleteBookAync(int bookId) {
            var book = new Books() { Id = bookId };

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        
        }
    }
}
