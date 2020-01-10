using System;
using System.Collections.Generic;

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

        public bool MakeMove(Turn turn) {
            Board.SetSquares(Board.Squares);
            if (!Board.HasWinner)
            {
                if (Board.Squares[turn.CellNumber] == Cell.Empty)
                {
                    Board.SetSquare(turn.CellNumber, Cell.Cross);
                    History.Turns.Add(Board);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Turn MakeAutoMove()
        {
            var possibleTurns = new List<int>(Board.GetEmptySquareIndexes());
           
            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            if (possibleTurns.Count > 0)
            {
                if (!Board.HasWinner)
                {
                    Board.SetSquare(Convert.ToInt32(possibleTurns[randomTurn]), Cell.Nought);
                    History.Turns.Add(Board);
                    return new Turn
                    {
                        CellNumber = Convert.ToInt32(possibleTurns[randomTurn]),
                        TicTurn = false
                    };
                }
                else
                {
                    return GetInvalidTurn();
                }
            }
            else
            {
                return GetInvalidTurn();
            }
        }

        private Turn GetInvalidTurn()
        {
            return new Turn
            {
                CellNumber = -1,
                TicTurn = false
            };
        }
    }
}
