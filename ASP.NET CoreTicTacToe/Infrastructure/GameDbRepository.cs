using ASP.NETCoreTicTacToe.Infrastructure;
using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETCoreTicTacToe.Models
{
    public class GameDbRepository
    {
        private IMapper mapper;
        private DbContextOptionsBuilder<TicTacToeContext> optionsBuilder;

        public GameDbRepository(IMapper mapper)
        {
            this.mapper = mapper;
            this.optionsBuilder = DBOptionsBuilder.Create();
        }

        public Dictionary<int, Game> GetAllGamesFromDatabase()
        {
            var result = new Dictionary<int, Game>();
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                var games = QueryGames(context);
                result = games.Select(item => new { GameDto = item, Game = mapper.Map<Game>(item) }).ToDictionary(x => x.GameDto.ID, x => x.Game);
            }
            return result;
        }

        public Game GetGameFromDatabase(int? id)
        {
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                var game = mapper.Map<Game>(QueryGames(context)
                    .FirstOrDefault(gameDTO => gameDTO.ID == id.Value));
                game = AddGameDependencies(id, game);
                return game;
            }
        }

        private IQueryable<GameDataTransferObject> QueryGames(TicTacToeContext context)
        {
            return context.Games
                .Include(gameDTO => gameDTO.History)
                .ThenInclude(history => history.Turns)
                .Include(gameDTO => gameDTO.Board)
                .Include(gameDTO => gameDTO.TicPlayer)
                .Include(gameDTO => gameDTO.TacPlayer);
        }

        private Game AddGameDependencies(int? id, Game game)
        {
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                if (game != null)
                {
                    var historyId = GetHistoryId(id.Value);
                    var history = mapper.Map<History>(context.Histories
                        .Include(historyDTO => historyDTO.Turns)
                        .FirstOrDefault(historyDTO => historyDTO.Id == historyId));
                    game.History = history;
                    var turns = context.Turns.Where(turn => turn.HistoryDataTransferObjectId == historyId).ToList();
                    turns.ForEach(turn => game.History.Turns.Add(mapper.Map<Turn>(turn)));
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
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                context.Games.Add(gameDataTransferObject);

                context.SaveChanges();
            }
            return gameDataTransferObject.ID;
           
        }

        public void UpdateGameInDatabase(Game game, int gameId)
        {
            
            
            var gameDTO = mapper.Map<GameDataTransferObject>(game);
            gameDTO.ID = gameId;
            
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                context.Games.Update(gameDTO);
                context.SaveChanges();
            }
               
            
        }

        private Guid GetHistoryId(int gameId)
        {
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                var query = context.Games
                    .Where(game => game.ID == gameId)
                    .Include(game => game.History)
                    .FirstOrDefault<GameDataTransferObject>();
                return query.History.Id;
            }
        }

        
    }
}
