using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class GameFarm
    {
        private static readonly GameFarm instance = new GameFarm();

        public static GameFarm Current => instance;

       
        private Dictionary<int, Game> games = new Dictionary<int, Game>();

        public Dictionary<int, Game> Games => games;


        int GetNewIdLocally(int? id)
        {
            if (id == null)
            {
                if (games.Count > 0)
                {
                    return games.Keys.Max() + 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return id.Value;
            }
        }

        public (int, Game) FindGameLocally(int? id) 
        {
            if (!id.HasValue || !games.TryGetValue(id.Value, out Game foundGame)) 
            {
                var newGame = new Game();
                newGame.InitHistory();
                newGame.InitBoard();
                int newId = GetNewIdLocally(id);
                
                games.Add(newId, newGame);
                return (newId, newGame);
            }
            
            return (id.Value, foundGame);
        }

        int GetNewId(int? id, TicTacToeContext database)
        {
            if (id == null)
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
            else
            {
                return id.Value;
            }
        }

        public (int, Game) GetGame(int? id, TicTacToeContext database)
        {
            Game gameInDatabase = null;
            if (id.HasValue)
            {
                gameInDatabase = database.Games
                    .Include(game => game.History)
                    .ThenInclude(history => history.Turns)
                    .Include(game => game.Board)
                    .FirstOrDefault(game => game.ID == id.Value);
                gameInDatabase.Board.SetSquares(gameInDatabase.History.RestoreBoardByTurnNumber(gameInDatabase.History.Turns.Count - 1).Squares);
            }
            if (!id.HasValue || gameInDatabase == null)
            {
                var newGame = new Game();
                newGame.InitHistory();
                newGame.InitBoard();
                int newId = GetNewId(id, database);
                if (games.ContainsKey(newId))
                {
                    games[newId] = newGame;
                }
                else
                {
                    games.Add(newId, newGame);
                }
                database.Games.Add(newGame);
                return (newId, newGame);
            }
            else
            {
                return (id.Value, gameInDatabase);
            }
        }
    }
}
