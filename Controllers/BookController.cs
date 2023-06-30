using BookStore.BookOperations.BookDetail;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.EditBook;
using BookStore.BookOperations.GetBooks;
using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.EditBook.EditBookCommand;

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
        public IActionResult GetById(int Id)
        {
            BookDetailQuery query = new BookDetailQuery(_dbContext);
            var result = query.Handle(Id);
            return Ok(result);
        }

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
        public  IActionResult updateBook(int id, [FromBody] EditBookModel updatedBook)
        {
            EditBookCommand command = new EditBookCommand(_dbContext);

            try
            {
                command.BookId = id;

                command.Model = updatedBook;
                command.Handle();


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
