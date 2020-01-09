using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Farm
    {
        private Dictionary<int, Game> games = new Dictionary<int, Game>();

        public Dictionary<int, Game> Games
        {
            get
            {
                return games;
            }
            set
            {
                games = value;
            }
        }

        int GetNewId(int? id)
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

        public (int, Game) FindGame(int? id) 
        {
            if (!id.HasValue || !games.TryGetValue(id.Value, out Game foundGame)) 
            {
                var newGame = new Game();

                int newId = GetNewId(id);
                
                games.Add(newId, newGame);
                return (newId, newGame);
            }
            
            return (id.Value, foundGame);
        }
    }
}
