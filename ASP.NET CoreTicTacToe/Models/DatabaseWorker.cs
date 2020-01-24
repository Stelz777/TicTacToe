using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
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
            var game = mapper.Map<Game>(database.Games
                .Include(game => game.History)
                .ThenInclude(history => history.Turns)
                .Include(game => game.Board)
                .FirstOrDefault(game => game.ID == id.Value));
            var turns = database.Turns.Where(turn => turn.HistoryId == game.History.ID).ToList();
            foreach (var turn in turns)
            {
                game.History.Turns.Add(mapper.Map<Turn>(turn));
            }
            return game;
        }

        public void AddGameToDatabase(Game newGame)
        {
            if (newGame != null)
            {
                var gameDataTransferObject = mapper.Map<GameDataTransferObject>(newGame);
                database.Games.Add(gameDataTransferObject);
                gameDataTransferObject.History.Turns[0].HistoryId = gameDataTransferObject.History.Id;
            }
        }

        public void AddTurnToDatabase(Turn turn, History history)
        {
            var turnDataTransferObject = mapper.Map<TurnDataTransferObject>(turn);
            var historyDataTransferObject = mapper.Map<HistoryDataTransferObject>(history);
            turnDataTransferObject.HistoryId = historyDataTransferObject.Id;
            database.Turns.Add(turnDataTransferObject);
        }

        public int GetNewId()
        {
            if (database.Games.Any())
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
