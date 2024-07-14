using AutoMapper;
using BookStore.DbOperations;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.Applicatiom.UserOperations.Commands.CreateUser
{
    public class CreateTokenCommand
    {
        readonly IBookStoreDbContext _dbContext;
        readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public CreateTokenModel Model { get; set; }
        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken  = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı email veya password hatalı");
            }

        }
    }
    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
