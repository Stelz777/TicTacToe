using ASP.NETCoreTicTacToe.Helpers;
using ASP.NETCoreTicTacToe.Infrastructure;
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
        private readonly AppSettings appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            if (appSettings != null)
            {
                this.appSettings = appSettings.Value;
            }
        }

        private string ConvertByteArrayToString(byte[] input)
        {
            var hex = new StringBuilder(input.Length * 2);
            foreach (var item in input)
            {
                hex.AppendFormat("{0:x2}", item);
            }
            return hex.ToString();
        }

        private byte[] ConvertStringToByteArray(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }

        public User Authenticate(UserAPI userAPI, string userName, string password)
        {
            if (password == null || userAPI == null)
            {
                return null;
            }
            var sha256 = new SHA256CryptoServiceProvider();
            
            var hashedPassword = ConvertByteArrayToString(
                sha256.ComputeHash(ConvertStringToByteArray(password)));

            sha256.Dispose();
            var user = userAPI.GetUserFromDatabase(userName);
            if (user == null)
            {
                return null;
            }
            if (user.Password != hashedPassword)
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

        public IEnumerable<User> GetAll(UserAPI userAPI)
        {
            if (userAPI == null)
            {
                return null;
            }
            return userAPI.GetAllUsersFromDatabase().Select(user =>
            {
                user.Password = null;
                return user;
            });
        }
    }
}
