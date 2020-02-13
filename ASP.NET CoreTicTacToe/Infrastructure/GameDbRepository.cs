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
        private TicTacToeContext context;

        public GameDbRepository(TicTacToeContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public Dictionary<int, Game> GetAllGamesFromDatabase()
        {
            var result = new Dictionary<int, Game>();
            
            var games = QueryGames(context);
            result = games.Select(item => new { GameDto = item, Game = mapper.Map<Game>(item) }).ToDictionary(x => x.GameDto.ID, x => x.Game);
            
            return result;
        }

        public Game GetGameFromDatabase(int? id)
        {
            var game = mapper.Map<Game>(QueryGames(context)
                .FirstOrDefault(gameDTO => gameDTO.ID == id.Value));
            return game;
        }

        private static IQueryable<GameDataTransferObject> QueryGames(TicTacToeContext context)
        {
            return context.Games
                .Include(gameDTO => gameDTO.History)
                .ThenInclude(history => history.Turns)
                .Include(gameDTO => gameDTO.Board)
                .Include(gameDTO => gameDTO.TicPlayer)
                .Include(gameDTO => gameDTO.TacPlayer);
        }

        public int AddGameToDatabase(Game newGame)
        {
            if (newGame == null)
            {
                throw new ArgumentNullException(nameof(newGame));
            }

            var gameDataTransferObject = mapper.Map<GameDataTransferObject>(newGame);
            
            context.Games.Add(gameDataTransferObject);

            context.SaveChanges();
            
            return gameDataTransferObject.ID;
        }

        public void UpdateGameInDatabase(Game game, int gameId)
        {
            var gameDTO = mapper.Map<GameDataTransferObject>(game);
            gameDTO.ID = gameId;
            
            context.Games.Update(gameDTO);
            context.SaveChanges();
        }
    }
}
