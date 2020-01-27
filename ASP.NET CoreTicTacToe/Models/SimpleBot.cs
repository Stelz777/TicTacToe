using ASP.NET_CoreTicTacToe.Models;
using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models
{
    public class SimpleBot : IBot
    {
        public bool IsActive { get; set; }
        public Side Side { get; set; }
        public Game Game { get; set; }

        public SimpleBot(Game game, Side side)
        {
            Game = game;
            Side = side;
        }

        public Turn MakeAutoMove()
        {
            var board = new Board();
            board.SetSquares(Game.Board.Squares);
            var possibleTurns = new List<int>(board.GetEmptySquareIndexes());
            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            if (possibleTurns.Count > 0)
            {
                if (!board.HasWinner)
                {
                    var validTurn = new Turn
                    {
                        CellNumber = Convert.ToInt32(possibleTurns[randomTurn]),
                        WhichTurn = Side
                    };
                    Game.MakeMove(validTurn);
                    return validTurn;
                }
                else
                {
                    return History.GetInvalidTurn();
                }
            }
            else
            {
                return History.GetInvalidTurn();
            }
        }
    }
}
