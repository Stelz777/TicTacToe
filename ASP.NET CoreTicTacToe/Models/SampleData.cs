using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public static class SampleData
    {
        public static void Initialize(TicTacToeContext context)
        {
            if (!context.Games.Any())
            {
                var game = new Game();
                game.InitHistory();
                game.InitBoard();
                context.Games.Add(game);
                context.SaveChanges();
            }
        }
    }
}
