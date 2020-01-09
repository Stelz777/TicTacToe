using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Game
    {
        public History History { get; set; }
        public Board Board { get; set; }

        public Game()
        {
            History = new History();
            Board = History.Turns[0];
        }
    }
}
