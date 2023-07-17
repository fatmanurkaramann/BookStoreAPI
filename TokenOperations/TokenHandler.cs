using BookStore.Entities;
using BookStore.TokenOperations.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookStore.TokenOperations
{
    public class TokenHandler
    {   
        public IConfiguration configuration { get; set; }

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token token = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration["Token:SecurityKey"]));

            //şifrelenmiş kimlik
            SigningCredentials signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            token.ExpireDate = DateTime.UtcNow.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: configuration["Token:Issuer"],
                audience: configuration["Token:Audience"],
                expires: token.ExpireDate,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );

            //token üretiliyor
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken= tokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();
            return token;

        }
        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
