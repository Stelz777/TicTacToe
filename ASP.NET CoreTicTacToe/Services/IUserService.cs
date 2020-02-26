using ASP.NETCoreTicTacToe.Infrastructure;
using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Services
{
    public interface IUserService
    {
        User Authenticate(UserAPI userAPI, string username, string password);
        IEnumerable<User> GetAll(UserAPI userAPI);
        User Register(UserAPI userAPI, string userName, string password);
    }
}
