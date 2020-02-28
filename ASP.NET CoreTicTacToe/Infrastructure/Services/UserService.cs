using ASP.NETCoreTicTacToe.Helpers;
using ASP.NETCoreTicTacToe.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ASP.NETCoreTicTacToe.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private UserAPI userAPI;

        public UserService(UserAPI userAPI, IOptions<AppSettings> appSettings)
        {
            this.userAPI = userAPI;
            if (appSettings != null)
            {
                this.appSettings = appSettings.Value;
            }
        }

        private static string ConvertByteArrayToString(byte[] input)
        {
            var hex = new StringBuilder(input.Length * 2);
            foreach (var item in input)
            {
                hex.AppendFormat("{0:x2}", item);
            }
            return hex.ToString();
        }

        private static byte[] ConvertStringToByteArray(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }

        public User Authenticate(UserForLogin userForLogin)
        {
            if (userForLogin.Password == null || userAPI == null)
            {
                return null;
            }
            var sha256 = new SHA256CryptoServiceProvider();
            
            var hashedPassword = ConvertByteArrayToString(
                sha256.ComputeHash(ConvertStringToByteArray(userForLogin.Password)));

            sha256.Dispose();
            var user = userAPI.GetUserFromDatabase(userForLogin.Name);
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

        public User Register(UserForRegistration userForRegistration)
        {
            if (userForRegistration.Password == null || userAPI == null)
            {
                return null;
            }
            var sha256 = new SHA256CryptoServiceProvider();

            var hashedPassword = ConvertByteArrayToString(
                sha256.ComputeHash(ConvertStringToByteArray(userForRegistration.Password)));

            sha256.Dispose();
            var user = userAPI.GetUserFromDatabase(userForRegistration.Name);
            if (user != null)
            {
                return null;
            }
            user = new User();
            user.Name = userForRegistration.Name;
            user.Password = hashedPassword;
            user.FirstName = userForRegistration.FirstName;
            user.LastName = userForRegistration.LastName;

            userAPI.AddUser(user);

            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAll()
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
