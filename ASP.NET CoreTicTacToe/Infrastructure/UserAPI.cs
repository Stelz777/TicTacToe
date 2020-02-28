using ASP.NETCoreTicTacToe.Models.Users;
using AutoMapper;
using System.Collections.Generic;

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
