using ASP.NETCoreTicTacToe.Helpers;
using ASP.NETCoreTicTacToe.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Services
{
    public class UserService : IUserService
    {
        private List<User> users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Name = "test", Password = "test" }
        };

        private readonly AppSettings appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            if (appSettings != null)
            {
                this.appSettings = appSettings.Value;
            }
        }

        public User Authenticate(string username, string password)
        {
            var user = users.SingleOrDefault(item => item.Name == username && item.Password == password);
            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return users.Select(user =>
            {
                user.Password = null;
                return user;
            });
        }
    }
}
