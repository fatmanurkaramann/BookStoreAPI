using AutoMapper;
using BookStore.Applicatiom.UserOperations.Commands.CreateUser;
using BookStore.Applicatiom.UserOperations.Commands.RefreshToken;
using BookStore.DbOperations;
using BookStore.TokenOperations.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController] //http response dönceği anlamına gelir
    [Route("[controller]s")]
    public class UserController : ApiController
    {
        readonly IConfiguration _config;
        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration config) : base(context, mapper)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_dbContext, _mapper);
            command.Model = newUser;
            command.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_dbContext,_mapper,_config);
            command.Model = login;
            var token = command.Handle();
            return token;
        }


        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _config);
            command.RefreshToken = token;
            var result = command.Handle();
            return result;
        }
    }
}
