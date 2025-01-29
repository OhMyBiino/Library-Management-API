using LibraryManagementAPI.Models.BookRepository;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementModels;

namespace LibraryManagementAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks()
        {
            try
            {
                var books = await _bookRepository.GetBooksAsync();

                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving books from the database.");
            }
        }

        [HttpGet("{Borrowed}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBorrowedBooks()
        {
            try
            {
                var borrowedBooks = await _bookRepository.GetBorrowedBooksAsync();

                if (borrowedBooks.Any()) 
                {
                    return Ok(borrowedBooks);
                    
                }
                return NotFound("No borrowed books.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving borrowed books from the database.");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<BookModel>> GetBookById([FromRoute]int Id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(Id);

                if (book == null)
                {
                    return NotFound($"Book with ID: {Id} not found.");
                }

                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving the book from the database.");
            }
        }

        [HttpGet("{search}/{Name}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> Search(string? Name, string? Genre)
        {
            try
            {
                var result = await _bookRepository.SearchAsync(Name, Genre);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookModel>> AddBookModel([FromBody]BookModel bookToAdd)
        {
            try
            {
                if (bookToAdd == null)
                {
                    return BadRequest();
                }

                //add custom validation, checking if the book already exist
                var existingBook = await _bookRepository.GetBookByIdAsync(bookToAdd.BookId);

                if (existingBook != null)
                {
                    return Conflict("Book already exists");
                }

                var addedBook = await _bookRepository.CreateBookAsync(bookToAdd);

                return CreatedAtAction(nameof(GetBookById), new { Id = addedBook.BookId }, addedBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding the book to the database.");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<BookModel>> UpdateBookModel([FromRoute]int Id, [FromBody]BookModel bookToUpdate)
        {
            try
            {
                if (Id != bookToUpdate.BookId)
                {
                    return BadRequest("Id mismatch");
                }

                var existingBook = await _bookRepository.GetBookByIdAsync(Id);

                if (existingBook == null)
                {
                    return NotFound($"Book with ID: {Id} cannot be found.");
                }

                var updatedBook = await _bookRepository.UpdateBookAsync(bookToUpdate);

                return Ok(updatedBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error updating the data from the database.");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<BookModel>> DeleteBookModel([FromRoute] int Id) 
        {
            try
            {
                var existingBook = await _bookRepository.GetBookByIdAsync(Id);

                if (existingBook == null) 
                {
                    return BadRequest($"Book with ID: {Id} could not be found.");
                }

                var deletedBook = await _bookRepository.DeleteBookAsync(Id);

                if (deletedBook == null) 
                {
                    return BadRequest();
                }

                return Ok(deletedBook);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                        "Error deleting the book from the database");
            }
        }
    }
}
