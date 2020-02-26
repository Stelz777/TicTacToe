using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Infrastructure
{
    public class UserAPI
    {
        private readonly GameDbRepository gameRepository;

        public UserAPI(TicTacToeContext context, IMapper mapper)
        {
            gameRepository = new GameDbRepository(context, mapper);
        }

        public List<User> GetAllUsersFromDatabase()
        {
            return gameRepository.GetAllUsersFromDatabase();
        }

        public User GetUserFromDatabase(string userName)
        {
            return gameRepository.GetUserFromDatabase(userName);
        }

        public void AddUser(User user)
        {
            gameRepository.AddUserToDatabase(user);
        }
    }
}
