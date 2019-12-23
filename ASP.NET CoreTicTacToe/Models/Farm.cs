using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Farm
    {
        private Dictionary<int, Board> boards;
        private int currentGame = 0;

        public Dictionary<int, Board> Boards
        {
            get
            {
                return boards;
            }
            set
            {
                boards = value;
            }
        }

        public int CurrentGame
        {
            get
            {
                return currentGame;
            }
            set
            {
                currentGame = value;
            }
        }

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
