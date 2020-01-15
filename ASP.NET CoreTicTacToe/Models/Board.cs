using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Board
    {
        //TODO реверсировать зависимость между ботом и геймом
        //передавать гейм, а не борд
        //сделать игру между ботами как юнит-тест

        private List<Cell> squares = new List<Cell>();
        public IReadOnlyList<Cell> Squares => squares;

        public Board()
        {
            squares.AddRange(Enumerable.Repeat(Cell.Empty, 9));
        }

        public void SetSquare(int cellNumber, Cell square)
        {
            squares[cellNumber] = square;
        }

        public void SetSquares(IReadOnlyList<Cell> squares)
        {
            this.squares = new List<Cell>(squares);
        }

        public bool HasWinner => TicTacToeRulesHelper.HasWinner(Squares);

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
    }   
}
