using AutoMapper;
using BookStore.BookOperations.BookDetail;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.EditBook;
using BookStore.BookOperations.GetBooks;
using BookStore.DbOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.EditBook.EditBookCommand;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStore.Controllers
{
    [ApiController] //http response dönceği anlamına gelir
    [Route("[controller]s")]
    public class BookController : ApiController
    {
        public BookController(IBookStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_dbContext, _mapper);
            var result = query.Handler();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            BookDetailQuery query = new BookDetailQuery(_dbContext,_mapper);
            query.BookId= Id;

            BookDetailValidator validations = new BookDetailValidator();
            validations.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult addBook([FromBody] CreateBookModel newBook)
        {
             CreateBookCommand command = new CreateBookCommand(_dbContext,_mapper);

           
                command.Model = newBook;
                CreateBookValidator validations = new CreateBookValidator();
               validations.ValidateAndThrow(command);
           
                command.Handle();


                return Ok();
         

        }
        [HttpPut("{id}")]
        public  IActionResult updateBook(int id, [FromBody] EditBookModel updatedBook)
        {
            EditBookCommand command = new EditBookCommand(_dbContext,_mapper);

            try
            {
                command.BookId = id;
                command.Model = updatedBook;

                UpdateBookValidator validations = new UpdateBookValidator();
                validations.ValidateAndThrow(command);

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
            DeleteBookCommand command = new DeleteBookCommand(_dbContext);
            command.BookId = Id;
            DeleteBookValidator validations = new DeleteBookValidator();
            validations.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
