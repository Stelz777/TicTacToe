using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class GameAPI
    {
        private DatabaseWorker databaseWorker;

        public GameAPI(TicTacToeContext database, IMapper mapper)
        {
            databaseWorker = new DatabaseWorker(database, mapper);
        }

        public int GetNewId()
        {
            return databaseWorker.GetNewId();
        }

        public Game GetGame(int? id)
        {
            return databaseWorker.GetGameFromDatabase(id);
        }

        public int AddGame(Game game)
        {
            return databaseWorker.AddGameToDatabase(game);
        }

        public void UpdateGame(Game game, int gameId)
        {
            databaseWorker.UpdateGameInDatabase(game, gameId);
        }
    }
}
