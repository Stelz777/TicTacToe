using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Board
    {
        private List<Square> squares = new List<Square>();

        [Key]
        public Guid ID { get; set; }
        public IReadOnlyList<Square> Squares => squares;
        public bool HasWinner => TicTacToeRulesHelper.HasWinner(Squares);

        public Board()
        {
            squares.AddRange(Enumerable.Range(1, 9).Select(item => new Square()).ToList());
        }

        public void SetSquare(int cellNumber, Cell cell)
        {
            squares[cellNumber].Cell = cell;
        }

        public void SetSquares(List<Square> squares)
        {
            this.squares = squares;
        }

        public void SetSquares(IReadOnlyList<Square> squares)
        {
            this.squares = (List<Square>) squares;
        }

        public IEnumerable<int> GetEmptySquareIndexes()
        {
            for (var i = 0; i < squares.Count; i++)
            {
                if (squares[i].Cell == Cell.Empty)
                {
                    yield return i;
                }
            }
        }   

        public Cell GetCellBySide(Side side)
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
