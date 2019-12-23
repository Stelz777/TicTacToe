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
        private List<Cell> squares = new List<Cell>();
        public IReadOnlyList<Cell> Squares => squares;

        public Board()
        {
            squares.AddRange(Enumerable.Repeat(Cell.Empty, 9));
        }

        public bool HasWinner()
        {
            var board = this;
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

        public Turn MakeAutoMove() {
            var possibleTurns = board.Squares.Where(sq => sq == Cell.Empty).ToList();
           
            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            if (possibleTurns.Count > 0)
            {
                return new Turn
                {
                    CellNumber = possibleTurns[randomTurn],
                    TicTurn = false
                };
            }
            else
            {
                return new Turn
                {
                    CellNumber = -1,
                    TicTurn = false
                };
            }
        }
    }

    
}
