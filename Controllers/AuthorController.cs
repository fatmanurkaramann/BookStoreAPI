using AutoMapper;
using BookStore.Applicatiom.AuthorOperations.Commands.CreateAuthor;
using BookStore.Applicatiom.AuthorOperations.Queries.GetAuthors;
using BookStore.Applicatiom.GenreOperations.Commands.CreateGenre;
using BookStore.Applicatiom.GenreOperations.Querys.GetGenres;
using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;

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
    }
}
