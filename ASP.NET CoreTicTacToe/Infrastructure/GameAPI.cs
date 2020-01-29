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

        public Game GetGameFromDatabase(int? id)
        {
            return gameRepository.GetGameFromDatabase(id);
        }

        public (int, Game) GetGame(int? id)
        {
            Game gameInDatabase = null;
            if (id.HasValue)
            {
                var (gameId, game) = gameFarm.GetGame(id);
                if (game != null)
                {
                    return (gameId, game);
                }
                gameInDatabase = GetGameFromDatabase(id);
                if (gameInDatabase != null)
                {
                    Board restoredBoard = gameInDatabase.History.RestoreBoardByTurnNumber(gameInDatabase.History.Turns.Count - 1);
                    gameInDatabase.Board.SetSquares(restoredBoard.Squares);
                }
            }

            if(!id.HasValue || gameInDatabase == null)
            {
                var newGame = new Game();                
                var newId = AddGame(newGame);
                gameFarm.AddGame(newId, newGame);
                
                return (newId, newGame);
            }
            else
            {
                return (id.Value, gameInDatabase);
            }
        }

        public int AddGame(Game game)
        {
            return gameRepository.AddGameToDatabase(game);
        }

        public void UpdateGame(Game game, int gameId)
        {
            if (game.CanContinue())
            {
                return;
            }
            gameRepository.UpdateGameInDatabase(game, gameId);
            gameFarm.ExcludeGame(gameId);
        }
    }
}
