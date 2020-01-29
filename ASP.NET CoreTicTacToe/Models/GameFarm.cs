using ASP.NETCoreTicTacToe.Models;
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

        public Game GetGame(int id)
        {
            if (games.TryGetValue(id, out var game))
            {
                return game;
            }

            return null;
        }
        
        public void AddGame(int newId, Game newGame)
        {
            if (games.ContainsKey(newId))
            {
                games[newId] = newGame;
            }
            else
            {
                games.Add(newId, newGame);
            }
        }

        public void ExcludeGame(int id)
        {
            if (games.ContainsKey(id))
            {
                games.Remove(id);
            }
        }
    }
}
