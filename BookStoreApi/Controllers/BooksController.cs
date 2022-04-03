using BookStoreApi.Models;
using BookStoreApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks() {

            var books = await _repository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById([FromRoute] int id) {

            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            else return Ok(book);

        }

        [HttpPost("")]
        public async Task<IActionResult> AddBook([FromBody] BookModel model) {

            var bookId = await _repository.AddBookAsync(model);
            return CreatedAtAction(nameof(GetBookById), new { id = bookId, Controller = "Books"}, bookId);
        
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookModel bookModel){
            _repository.UpdateBookAsync(id, bookModel);
            return Ok();
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id) {
            await _repository.DeleteBookAync(id);
            return NoContent();
        
        }


    }
}
