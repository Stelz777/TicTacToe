using ASP.NETCoreTicTacToe.Models;
using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class GameDbRepository
    {
        private TicTacToeContext database;
        private IMapper mapper;
        

        public GameDbRepository(TicTacToeContext database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public Game GetGameFromDatabase(int? id)
        {
            var game = mapper.Map<Game>(database.Games
                .Include(gameDTO => gameDTO.History)
                .ThenInclude(history => history.Turns)
                .Include(gameDTO => gameDTO.Board)
                .Include(gameDTO => gameDTO.TicPlayer)
                .Include(gameDTO => gameDTO.TacPlayer)
                .FirstOrDefault(gameDTO => gameDTO.ID == id.Value));
            if (game != null)
            {
                var historyId = GetHistoryId(id.Value);
                var history = mapper.Map<History>(database.Histories
                    .Include(historyDTO => historyDTO.Turns)
                    .FirstOrDefault(historyDTO => historyDTO.Id == historyId));
                game.History = history;
                var turns = database.Turns.Where(turn => turn.HistoryDataTransferObjectId == historyId).ToList();
                foreach (var turn in turns)
                {
                    game.History.Turns.Add(mapper.Map<Turn>(turn));
                }
            }
            return game;
        }

        public int AddGameToDatabase(Game newGame)
        {
            if (newGame == null)
            {
                throw new ArgumentNullException(nameof(newGame));
            }

            var gameDataTransferObject = mapper.Map<GameDataTransferObject>(newGame);
            database.Games.Add(gameDataTransferObject);

            database.SaveChanges();
            return gameDataTransferObject.ID;
           
        }

        public void UpdateGameInDatabase(Game game, int gameId)
        {
            
            
            var gameDTO = mapper.Map<GameDataTransferObject>(game);
            gameDTO.ID = gameId;
            
            var optionsBuilder = CreateOptionsBuilder();
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                context.Games.Update(gameDTO);
                context.SaveChanges();
            }
               
            
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


        public static Guid GetHistoryId(int gameId)
        {
            var optionsBuilder = CreateOptionsBuilder();
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                var query = context.Games
                    .Where(game => game.ID == gameId)
                    .Include(game => game.History)
                    .FirstOrDefault<GameDataTransferObject>();
                return query.History.Id;
            }
        }

        public static DbContextOptionsBuilder<TicTacToeContext> CreateOptionsBuilder()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder;
        }
    }
}
