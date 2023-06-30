using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBooks;
using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.Controllers
{
    [ApiController] //http response dönceği anlamına gelir
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        readonly BookStoreDbContext _dbContext;
        public BookController(BookStoreDbContext context)
        {
            _dbContext = context;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_dbContext);
            var result = query.Handler();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public Book GetById(int Id)
        {
            var bookList = _dbContext.Books.Where(x => x.Id == Id).SingleOrDefault();
            return bookList;
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = books.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();

        //    return book;
        //}

        [HttpPost]
        public IActionResult addBook([FromBody] CreateBookModel newBook)
        {
             CreateBookCommand command = new CreateBookCommand(_dbContext);

            try
            {
                command.Model = newBook;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id}")]
        public  IActionResult updateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
            {
                return BadRequest();
            }

            book.Title = updatedBook.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult deleteBook(int Id)
        {
            var book= _dbContext.Books.SingleOrDefault(x=>x.Id==Id);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
