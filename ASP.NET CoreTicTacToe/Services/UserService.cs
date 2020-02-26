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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Services
{
    public class UserService : IUserService
    {
        
        
        private List<User> users = new List<User>();
       

        private readonly AppSettings appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            if (appSettings != null)
            {
                this.appSettings = appSettings.Value;
            }
            var sha256 = new SHA256CryptoServiceProvider();
            var password = sha256.ComputeHash(ConvertStringToByteArray("test"));
            sha256.Dispose();
            var user = new User()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Name = "test"
            };
            user.Password = ConvertByteArrayToString(password);
            users.Add(user);
        }

        private string ConvertByteArrayToString(byte[] input)
        {
            return Encoding.ASCII.GetString(input, 0, input.Length);
        }

        private byte[] ConvertStringToByteArray(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }

        public User Authenticate(string username, string password)
        {
            if (password == null)
            {
                return null;
            }
            var sha256 = new SHA256CryptoServiceProvider();

            var hashedPassword = ConvertByteArrayToString(
                sha256.ComputeHash(ConvertStringToByteArray(password)));

            var user = users.SingleOrDefault(item => 
                item.Name == username && 
                item.Password == hashedPassword);

            sha256.Dispose();
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
