using ASP.NETCoreTicTacToe.Models.Users;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Infrastructure.Services
{
    public interface IUserService
    {
        User Authenticate(UserForLogin userForLogin);
        IEnumerable<User> GetAll();
        User Register(UserForRegistration userForRegistration);
    }
}
