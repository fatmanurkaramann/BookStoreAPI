using AutoMapper;
using BookStore.Applicatiom.GenreOperations.Commands.CreateGenre;
using BookStore.Applicatiom.GenreOperations.Commands.DeleteGenre;
using BookStore.Applicatiom.GenreOperations.Commands.UpdateGenre;
using BookStore.Applicatiom.GenreOperations.Querys.GenreDetail;
using BookStore.Applicatiom.GenreOperations.Querys.GetGenres;
using BookStore.BookOperations.BookDetail;
using BookStore.BookOperations.CreateBook;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.EditBook.EditBookCommand;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStore.Controllers
{
    [ApiController] //http response dönceği anlamına gelir
    [Route("[controller]s")]
    public class GenreController:ApiController
    {
        public GenreController(IBookStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        public ActionResult GetGenresQuery()
        {
            GetGenreQuery getGenre =new GetGenreQuery(_dbContext,_mapper);
            return Ok(getGenre.Handle());
        }

        [HttpGet("{id}")]
        public ActionResult GetGenreDetail(int id)
        {
            GenreDetailQuery getGenre = new GenreDetailQuery(_dbContext, _mapper);
            getGenre.GenreId = id;

            GenreDetailValidator validations = new GenreDetailValidator();
            validations.ValidateAndThrow(getGenre);

            return Ok(getGenre.Handle());

        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreViewModel newGenre)
        {
            CreateGenreCommand createGenre = new CreateGenreCommand(_dbContext,_mapper);
            createGenre.CreateViewModel = newGenre;
            CreateGenreValidator validations = new CreateGenreValidator();
            validations.ValidateAndThrow(createGenre);
            createGenre.Handle();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreViewModel updateGenre)
        {
            UpdateGenreCommand update = new UpdateGenreCommand(_dbContext,_mapper);
            update.GenreId = id;
            update.model = updateGenre;

            UpdateGenreValidator validationRules = new UpdateGenreValidator();
            validationRules.ValidateAndThrow(update);

            update.Handle();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand delete = new DeleteGenreCommand(_dbContext);
            delete.GenreId = id;


            delete.Handle();
            return Ok();

        }
    }
}
