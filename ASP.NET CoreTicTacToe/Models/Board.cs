using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Board
    {
        private List<Cell> squares = new List<Cell>();

        [Key]
        public Guid ID { get; set; }
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
    }   
}
