using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.Applicatiom.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        readonly IBookStoreDbContext _dbContext;
        readonly IConfiguration _configuration;
        public string RefreshToken { get; set; }
        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && 
                         x.RefreshTokenExpireDate>DateTime.UtcNow);

            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid refresh token bulunamadı");
            }
        }
    }
}
