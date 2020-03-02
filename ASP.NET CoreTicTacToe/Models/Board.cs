using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Board
    {
        private List<Cell> squares = new List<Cell>();

        
        public IReadOnlyList<Cell> Squares => squares;
        public bool HasWinner => TicTacToeRulesHelper.HasWinner(Squares);

        

        public Board()
        {
            squares.AddRange(Enumerable.Repeat(Cell.Empty, 9));
        }

        public void SetSquare(int cellNumber, Cell cell)
        {
            squares[cellNumber] = cell;
        }

        public void SetSquares(IReadOnlyList<Cell> squares)
        {
            this.squares = new List<Cell>(squares);
        }

        public IEnumerable<int> GetEmptySquareIndexes()
        {
            for (var i = 0; i < squares.Count; i++)
            {
                if (squares[i] == Cell.Empty)
                {
                    yield return i;
                }
            }
        }   

        public static Cell GetCellBySide(Side side)
        {
            if (side == Side.Tic)
            {
                return Cell.Cross;
            }
            else
            {
                return Cell.Nought;
            }
        }

        public static Board GenerateSimulatedBoard(Board board, int abscissa, int ordinate, Side side)
        {
            if (board == null)
            {
                return null;
            }
            var newBoard = Enumerable.Range(0, 9).Select(_ => Cell.Empty).ToList();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    newBoard[i * 3 + j] = board.Squares[i * 3 + j];
                }
            }
            newBoard[abscissa * 3 + ordinate] = GetCellBySide(side);
            var simulatedBoard = new Board();
            simulatedBoard.SetSquares(newBoard);
            return simulatedBoard;
        }

        public bool HasWon(Side side)
        {
            if (!IsEmpty() 
                && TicTacToeRulesHelper.HasWonVertically(squares, side) 
                || TicTacToeRulesHelper.HasWonHorizontally(squares, side) 
                || TicTacToeRulesHelper.HasWonDiagonally(squares, side))
            {
                return true;
            }
            return false;
        }

        private bool IsEmpty()
        {
            if (Squares.Contains(Cell.Cross) || Squares.Contains(Cell.Nought))
            {
                return false;
            }
            return true;
        }
    }   
}
