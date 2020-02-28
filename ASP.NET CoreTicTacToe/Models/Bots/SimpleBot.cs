using ASP.NETCoreTicTacToe.Models.Games;
using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Models.Bots
{
    public class SimpleBot : IBot
    {
        public SimpleBot(Side side)
        {
            Side = side;
        }

        public Side Side { get; private set; }

        public string Name => "Simple";
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
                        CellNumber = possibleTurns[randomTurn],
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
