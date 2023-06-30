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

namespace BookStore.Controllers
{
    [ApiController] //http response dönceği anlamına gelir
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
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
            var result = query.Handle(Id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult addBook([FromBody] CreateBookModel newBook)
        {
             CreateBookCommand command = new CreateBookCommand(_dbContext,_mapper);

            try
            {
                command.Model = newBook;
                CreateBookValidator validations = new CreateBookValidator();
               validations.ValidateAndThrow(command);
                //if (!result.IsValid)
                //{
                //    foreach (var err in result.Errors)
                //    {
                //        Console.WriteLine(err.PropertyName+"-"+err.ErrorMessage);
                //    }
                //}
                //else
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
            EditBookCommand command = new EditBookCommand(_dbContext,_mapper);

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
            DeleteBookCommand command = new DeleteBookCommand(_dbContext);
            command.BookId = Id;
            DeleteBookValidator validations = new DeleteBookValidator();
            validations.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
