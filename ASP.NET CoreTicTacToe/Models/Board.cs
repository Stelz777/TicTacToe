using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Board
    {
        private List<Square> squares = new List<Square>();
        public IReadOnlyList<Square> Squares => squares;

        [Key]
        public Guid ID { get; set; }


        public Board()
        {
            squares.AddRange(Enumerable.Repeat(new Square(), 9));
        }

        public void SetSquare(int cellNumber, Cell square)
        {
            squares[cellNumber].Cell = square;
        }

        public void SetSquares(List<Square> squares)
        {
            this.squares = squares;
            
        }

        public bool HasWinner => TicTacToeRulesHelper.HasWinner(Squares);

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
