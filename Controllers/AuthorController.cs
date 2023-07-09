using AutoMapper;
using BookStore.Applicatiom.AuthorOperations.Commands.CreateAuthor;
using BookStore.Applicatiom.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Applicatiom.AuthorOperations.Queries.GetAuthors;
using BookStore.Applicatiom.GenreOperations.Commands.CreateGenre;
using BookStore.Applicatiom.GenreOperations.Querys.GetGenres;
using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;
using static BookStore.BookOperations.EditBook.EditBookCommand;

namespace BookStore.Controllers
{
    [ApiController] //http response dönceği anlamına gelir
    [Route("[controller]s")]
    public class AuthorController:ControllerBase
    {
        readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery getAuthor = new GetAuthorsQuery(_dbContext, _mapper);
            return Ok(getAuthor.Handle());
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand createAuthor = new CreateAuthorCommand(_dbContext, _mapper);
            createAuthor.CreateAuthorModel = newAuthor;

            createAuthor.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthorModel)
        {
            UpdateAuthorCommand updateAuthor = new UpdateAuthorCommand(_dbContext,_mapper);
            updateAuthor.AuthorId = id;
            updateAuthor.Model=updateAuthorModel;
            updateAuthor.Handle();
            return Ok();
        }
    }
}
