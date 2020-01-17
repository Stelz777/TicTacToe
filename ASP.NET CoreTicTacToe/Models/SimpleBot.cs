using System;
using System.Collections.Generic;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class SimpleBot : IBot
    {
        public bool isActive;
        public Side side;
        public Game game;

        public SimpleBot(Game game, Side side)
        {
            this.game = game;
            this.side = side;
        }

        public Turn MakeAutoMove()
        {
            var board = new Board();
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
                        WhichTurn = side
                    };
                    game.MakeMove(validTurn);
                    return validTurn;
                }
                else
                {
                    return game.GetInvalidTurn();
                }
            }
            else
            {
                return game.GetInvalidTurn();
            }
        }
    }
}
