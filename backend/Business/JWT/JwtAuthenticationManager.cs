using Business.Concrete;
using Repository.Concrete;
using Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Business.JWT
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private TwitterContext twitterContext;
        private string key = "Bu hash için oluşturulan key";

        public JwtAuthenticationManager(TwitterContext _twitterContext)
        {
            twitterContext = _twitterContext;
        }

        public JwtResponse CreateToken(JwtRequest jwtRequest)
        {
            User user = twitterContext.Users.Where(x => x.Mail == jwtRequest.Mail).FirstOrDefault();
            if (user != null && BCryptNet.Verify(jwtRequest.Password, user.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim(ClaimTypes.Name, jwtRequest.Password),
                        new Claim(ClaimTypes.Email, jwtRequest.Mail)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new JwtResponse
                {
                    Token = tokenHandler.WriteToken(token),
                    User = user
                };
                  
            }
            return null;

        }
    }
}
