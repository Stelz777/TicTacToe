using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class DatabaseWorker
    {
        private TicTacToeContext database;
        private IMapper mapper;

        public DatabaseWorker(TicTacToeContext database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public Game GetGameFromDatabase(int? id)
        {
            return mapper.Map<Game>(database.Games
                .Include(game => game.History)
                .ThenInclude(history => history.Turns)
                .Include(game => game.Board)
                .FirstOrDefault(game => game.ID == id.Value));
        }

        public void AddGameToDatabase(Game newGame)
        {
            var gameDataTransferObject = mapper.Map<GameDataTransferObject>(newGame);
            database.Games.Add(gameDataTransferObject);
        }

        public int GetNewId()
        {
            if (database.Games.Count() > 0)
            {
                return database.Games.Max(entry => entry.ID) + 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
