using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Farm
    {
        public Dictionary<int, Board> boards;
        public int currentGame = 0;

        public Farm()
        {
            boards = new Dictionary<int, Board>();
            Board board = new Board();
            board.Squares = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                board.Squares.Add(Cell.Empty);
            }
            boards.Add(0, board);
        }
    }
}
