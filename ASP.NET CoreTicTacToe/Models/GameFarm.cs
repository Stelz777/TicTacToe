using ASP.NET_CoreTicTacToe.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETCoreTicTacToe.Models
{
    public class GameFarm
    {
      
        private Dictionary<int, Game> games = new Dictionary<int, Game>();

        public Dictionary<int, Game> Games => games;

        private GameAPI gameAPI;

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

        public (int, Game) GetGame(int? id, GameAPI gameAPI)
        {
            this.gameAPI = gameAPI;
            Game gameInDatabase = null;
            if (id.HasValue)
            {
                if (games.ContainsKey(id.Value))
                {
                    return (id.Value, games[id.Value]);
                }
                if (gameAPI != null)
                {
                    gameInDatabase = gameAPI.GetGame(id);
                    if (gameInDatabase != null)
                    {
                        Board restoredBoard = gameInDatabase.History.RestoreBoardByTurnNumber(gameInDatabase.History.Turns.Count - 1);
                        gameInDatabase.Board.SetSquares(restoredBoard.Squares);
                    }
                }
            }
            if (!id.HasValue || gameInDatabase == null)
            {
                var newGame = new Game();
                newGame.InitHistory();
                newGame.InitBoard();
                int newId = GetNewId(id);
                if (games.ContainsKey(newId))
                {
                    games[newId] = newGame;
                }
                else
                {
                    games.Add(newId, newGame);
                }
                if (gameAPI != null)
                {
                    newId = gameAPI.AddGame(newGame);
                }
                return (newId, newGame);
            }
            else
            {
                return (id.Value, gameInDatabase);
            }
        }

        int GetNewId(int? id)
        {
            if (id == null)
            {
                return gameAPI.GetNewId();
            }
            else
            {
                return id.Value;
            }
        }
    }
}
