using AutoMapper;
using BookStore.Applicatiom.AuthorOperations.Commands.CreateAuthor;
using BookStore.Applicatiom.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Applicatiom.AuthorOperations.Queries.AuthorDetail;
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
    public class AuthorController:ApiController
    {
        public AuthorController(IBookStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
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

        [HttpGet("{id}")]
        public IActionResult AuthorDetail(int id)
        {
            AuthorDetailQuery authorDetail = new AuthorDetailQuery(_mapper,_dbContext);
            authorDetail.AuthorId = id;
            var result = authorDetail.Handle();
            return Ok(result);
        }
    }
}
