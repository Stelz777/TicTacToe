using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public enum Cell
    {
        Cross,
        Nought,
        Empty
    }


    public class Board
    {
        public List<Cell> Squares { get; set; }

        public bool HasWinner(Board board)
        {
            for (int i = 0; i < 9; i += 3)
            {
                if (board.Squares[i] != Cell.Empty && board.Squares[i] == board.Squares[i + 1] && board.Squares[i + 1] == board.Squares[i + 2])
                {
                    return true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (board.Squares[i] != Cell.Empty && board.Squares[i] == board.Squares[i + 3] && board.Squares[i + 3] == board.Squares[i + 6])
                {
                    return true;
                }
            }
            if (board.Squares[0] != Cell.Empty && board.Squares[0] == board.Squares[4] && board.Squares[4] == board.Squares[8])
            {
                return true;
            }
            if (board.Squares[2] != Cell.Empty && board.Squares[2] == board.Squares[4] && board.Squares[4] == board.Squares[6])
            {
                return true;
            }
            return false;

        }
    }

    
}
