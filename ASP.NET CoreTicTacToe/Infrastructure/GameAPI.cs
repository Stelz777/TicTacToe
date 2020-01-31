using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class GameAPI
    {
        private readonly GameDbRepository gameRepository;
        private readonly GameFarm gameFarm;

        public GameAPI(GameFarm gameFarm, TicTacToeContext database, IMapper mapper)
        {
            gameRepository = new GameDbRepository(database, mapper);
            this.gameFarm = gameFarm;
        }

        public (int, Game) GetGame(int? id)
        {
            if (id.HasValue)
            {
                var game = gameFarm.GetGame(id.Value);
                if (game != null)
                {
                    return (id.Value, game);
                }
                game = GetGameFromDatabase(id);
                if (game == null)
                {
                    throw new InvalidOperationException("A game not found by specified id in game farm or db");
                }
                return (id.Value, game);
            }

            var newGame = new Game();                
            var newId = AddGame(newGame);
            gameFarm.AddGame(newId, newGame);
                
            return (newId, newGame);
        }

        public void UpdateGame(Game game, int gameId)
        {
            if (game != null)
            {
                if (game.CanContinue())
                {
                    return;
                }
                gameRepository.UpdateGameInDatabase(game, gameId);
                gameFarm.ExcludeGame(gameId);
            }
        }

        private Game GetGameFromDatabase(int? id)
        {
            return gameRepository.GetGameFromDatabase(id);
        }

        private int AddGame(Game game)
        {
            return gameRepository.AddGameToDatabase(game);
        }

    }
}
