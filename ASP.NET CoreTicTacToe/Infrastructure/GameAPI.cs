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
        private readonly GameFarm gameFarm;


        public GameAPI(GameFarm gameFarm, TicTacToeContext database, IMapper mapper)
        {
            databaseWorker = new DatabaseWorker(database, mapper);
            this.gameFarm = gameFarm;
        }

        public int GetNewId(int? id)
        {    
            if (id == null)
            {
                return databaseWorker.GetNewId();
            }
            else
            {
                return id.Value;
            }
        }

        public Game GetGameFromDatabase(int? id)
        {
            return databaseWorker.GetGameFromDatabase(id);
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
                newGame.InitHistory();
                newGame.InitBoard();
                int newId = GetNewId(id);
                gameFarm.AddGame(newId, newGame);
                
                newId = AddGame(newGame);
                
                return (newId, newGame);
            }
            else
            {
                return (id.Value, gameInDatabase);
            }
        }

        public int AddGame(Game game)
        {
            return databaseWorker.AddGameToDatabase(game);
        }

        public void UpdateGame(Game game, int gameId)
        {
            databaseWorker.UpdateGameInDatabase(game, gameId);
            gameFarm.ExcludeGame(gameId);
        }
    }
}
