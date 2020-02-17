using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models
{
    public class SimpleBot : Player, IBot
    {
        public SimpleBot()
        {
            Name = "S1mple";
        }

        public SimpleBot(Side side)
            :this()
        {
            Side = side;
        }

        public Turn MakeAutoMove(Game game)
        {
            var board = new Board();
            if (game == null)
            {
                return null;
            }
            board.SetSquares(game.Board.Squares);
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
                        Side = Side
                    };
                    game.MakeMove(validTurn);
                    return validTurn;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
